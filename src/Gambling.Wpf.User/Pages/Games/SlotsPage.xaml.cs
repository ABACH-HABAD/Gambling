using System.Windows;
using System.Windows.Controls;
using Gambling.Application.Core.Abstractions.Game.Slots;
using Gambling.Application.Core.Api.Results;
using Gambling.Core.Exceptions;
using Gambling.Wpf.User.Abstractions;
using Gambling.Wpf.User.Services.SlotsServices;
using Gambling.Wpf.User.Windows;

namespace Gambling.Wpf.User.Pages.Games;

/// <summary>
/// Логика взаимодействия для Slots.xaml
/// </summary>
public partial class SlotsPage : Page
{
    private readonly INavigationService _navigationService;
    private readonly ISlotsService _slotService;
    private readonly ISlotViewer _slotViewer;
    private readonly IElementAnimator _slotAnimator;

    private readonly List<UIElement> Clickable = [];

    public SlotsPage(INavigationService navigationService, ISlotsService slotsService, ISlotsCombinationCounterService slotsCombinationCounterService)
    {
        _navigationService = navigationService;
        _navigationService.SetFrame(AuthWindow.Instance.MainFrame);
        _slotService = slotsService;
        _slotAnimator = new SlotAnimator();
        _slotViewer = new SlotViewer(_slotAnimator, slotsCombinationCounterService);

        InitializeComponent();

        Clickable.Add(Bet100);
        Clickable.Add(Bet500);
        Clickable.Add(Bet1000);
        Clickable.Add(HalfBet);
        Clickable.Add(MaxBet);
        Clickable.Add(DropBet);
        Clickable.Add(Bid);
        Clickable.Add(Spin);
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await _slotViewer.ViewSlotsWithoutAnimation(SlotsGrid, await _slotViewer.ViewSlots(_slotViewer.StartSlots));
    }

    private async void Spin_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        await SpinSlotsAsync();
    }

    private async Task SpinSlotsAsync()
    {
        foreach (UIElement element in Clickable) element.IsEnabled = false;

        if (await _slotService.HasBonusGamesAsync(0)) MessageBox.Show("Запущена бонусная игра");

        if (!double.TryParse(Bid.Text, out double bidValue) || bidValue <= 0)
        {
            MessageBox.Show("Ставка не корректная");
            goto endgame;
        }

        SlotGameResult? gameResult;
        try
        {
            gameResult = await _slotService.SpinAsync(AuthWindow.Instance.CurrentUser.Id, bidValue, 3, 5);
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
        await AuthWindow.Instance.UpdateProfileInfo();

        if (await _slotService.HasBonusGamesAsync(AuthWindow.Instance.CurrentUser.Id)) await SpinSlotsAsync();

    endgame:
        foreach (UIElement element in Clickable) element.IsEnabled = true;

        await AuthWindow.Instance.UpdateProfileInfo();
    }

    private void AddToBet(double count)
    {
        if (double.TryParse(Bid.Text, out double bet))
        {
            SetBet(bet + count);
        }
        else SetBet(count);
    }

    private void SetBet(double count) => Bid.Text = count.ToString();

    private void Bet100_Click(object sender, RoutedEventArgs e) => AddToBet(100);

    private void Bet500_Click(object sender, RoutedEventArgs e) => AddToBet(500);

    private void Bet1000_Click(object sender, RoutedEventArgs e) => AddToBet(1000);

    private void HalfBet_Click(object sender, RoutedEventArgs e) => SetBet(Math.Round(AuthWindow.Instance.CurrentBalance / 2));

    private void MaxBet_Click(object sender, RoutedEventArgs e) => SetBet(AuthWindow.Instance.CurrentBalance);

    private void DropBet_Click(object sender, RoutedEventArgs e) => SetBet(0);
}