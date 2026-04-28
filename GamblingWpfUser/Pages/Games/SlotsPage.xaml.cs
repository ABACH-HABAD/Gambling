using BusinessLogic.Game.Slots;
using DataBaseClasses;
using GamblingWpfUser.Navigation;
using GamblingWpfUser.SlotsServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GamblingWpfUser.Pages.Games;

/// <summary>
/// Логика взаимодействия для Slots.xaml
/// </summary>
public partial class SlotsPage : Page
{
    private readonly INavigationService _navigationService;
    private readonly ISlotsService _slotService;
    private readonly ISlotViewer _slotViewer;
    private readonly IElementAnimator _slotAnimator;

    public SlotsPage(INavigationService navigationService, ISlotsService slotsService, ISlotsCombinationCounterService slotsCombinationCounterService)
    {
        _navigationService = navigationService;
        _navigationService.SetFrame(MainWindow.Instance.MainFrame);
        _slotService = slotsService;
        _slotAnimator = new SlotAnimator();
        _slotViewer = new SlotViewer(_slotAnimator, slotsCombinationCounterService);

        InitializeComponent();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await _slotViewer.ViewSlotsWithoutAnimation(SlotsGrid, await _slotViewer.ViewSlots(_slotViewer.StartSlots));
    }

    private async void Spin_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        await SpinSlots();
    }

    private async Task SpinSlots()
    {
        Bid.IsEnabled = false;
        Spin.IsEnabled = false;

        if (await _slotService.HasBonusGames(0)) MessageBox.Show("Запущена бонусная игра");

        if (!double.TryParse(Bid.Text, out double bidValue))
        {
            MessageBox.Show("Ставка не корректная");
            goto endgame;
        }

        SlotGameResult? gameResult;
        try
        {
            gameResult = await _slotService.Spin(0, bidValue, 3, 5);
            if (gameResult == null)
            {
                MessageBox.Show($"Во время игры мы зафиксировали попытку вмешательства в игровой процесс!\nДля честности игра была отменена\nВсе средства отправлены на счёт казино");
                goto endgame;
            }
            if (!gameResult.Result)
            {
                MessageBox.Show(gameResult.Message);
                goto endgame;
            }
        }
        catch (InsufficientFundsException)
        {
            MessageBox.Show("На балансе недостаточно средств");
            goto endgame;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Во время игры мы зафиксировали попытку вмешательства в игровой процесс!\nДля честности игра была отменена\nВсе средства отправлены на счёт казино\n\nПодробнее:\n{ex.Message}");
            goto endgame;
        }
        
        double duration = 0.01;
        for (double i = 0.6; duration < 0.7; i += 0.015)
        {
            duration = Math.Pow(i, 10);
            if (duration < 0.1) duration = 0.1;
            await _slotViewer.ViewSpinSlots(SlotsGrid, await _slotViewer.GetCurrentElements(SlotsGrid), await _slotViewer.ViewSlots(_slotViewer.GenerateRandomSlots()), duration: duration);
        }
        await _slotViewer.ViewSpinSlots(SlotsGrid, await _slotViewer.GetCurrentElements(SlotsGrid), await _slotViewer.ViewSlots(gameResult.Elements, drawCombos: true), duration: 0.8);
        

        if (gameResult.Win > 0) MessageBox.Show($"Вы выиграли {gameResult.Win}");
        MainWindow.Instance.UpdateProfileInfo();

        if (await _slotService.HasBonusGames(0)) await SpinSlots();

    endgame:
        Bid.IsEnabled = true;
        Spin.IsEnabled = true;

        MainWindow.Instance.UpdateProfileInfo();
    }
}
