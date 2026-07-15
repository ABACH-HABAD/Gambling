using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Gambling.Core.Exceptions;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.Abstractions.Game.Roulette;
using Gambling.Application.Core.BusinessModels.GameModels.Roulette;
using Gambling.Wpf.User.Services.RouletteServices;
using Gambling.Wpf.User.Abstractions;
using Gambling.Wpf.User.Windows;

namespace Gambling.Wpf.User.Pages.Games;

/// <summary>
/// Логика взаимодействия для RoulettePage.xaml
/// </summary>
public partial class RoulettePage : Page
{
    private readonly IRouletteService _rouletteService;
    private readonly IElementAnimator _rouletteAnimatorService;

    private static readonly int[] numbers =
        [0, 26, 3, 35, 12, 28, 7, 29, 18, 22, 9, 31, 14, 20, 1, 33, 16, 24, 5, 10, 23, 8, 30, 11, 36, 13, 27, 6, 34, 17, 25, 2, 21, 4, 19, 15, 32];
    //[0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 21, 13, 36, 11, 30, 8, 32, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26];

    private static readonly Color SELECTED_COLOR = Color.FromRgb(200, 200, 0);
    private readonly SolidColorBrush SELECTED_BRUSH = new(SELECTED_COLOR);
    private SolidColorBrush? BufferBrush;
    private Button? SelectedButton;

    private readonly ObservableCollection<RouletteBid> RouletteBids = [];
    private readonly List<RouletteBid> LastBids = [];
    private RouletteBid? TempBid;

    private readonly List<UIElement> Clickable = [];

    public RoulettePage(IRouletteService rouletteService)
    {
        _rouletteService = rouletteService;
        _rouletteAnimatorService = new RouletteAnimator();

        InitializeComponent();

        Clickable.Add(Spin);
        Clickable.Add(Accept);
        Clickable.Add(LastBid);
        Clickable.Add(Bid);
        Clickable.Add(Bids);
        Clickable.Add(Zero);
        Clickable.Add(Number1);
        Clickable.Add(Number2);
        Clickable.Add(Number3);
        Clickable.Add(Number4);
        Clickable.Add(Number5);
        Clickable.Add(Number6);
        Clickable.Add(Number7);
        Clickable.Add(Number8);
        Clickable.Add(Number9);
        Clickable.Add(Number10);
        Clickable.Add(Number11);
        Clickable.Add(Number12);
        Clickable.Add(Number13);
        Clickable.Add(Number14);
        Clickable.Add(Number15);
        Clickable.Add(Number16);
        Clickable.Add(Number17);
        Clickable.Add(Number18);
        Clickable.Add(Number19);
        Clickable.Add(Number20);
        Clickable.Add(Number21);
        Clickable.Add(Number22);
        Clickable.Add(Number23);
        Clickable.Add(Number24);
        Clickable.Add(Number25);
        Clickable.Add(Number26);
        Clickable.Add(Number27);
        Clickable.Add(Number28);
        Clickable.Add(Number29);
        Clickable.Add(Number30);
        Clickable.Add(Number31);
        Clickable.Add(Number32);
        Clickable.Add(Number33);
        Clickable.Add(Number34);
        Clickable.Add(Number35);
        Clickable.Add(Number36);
        Clickable.Add(First12);
        Clickable.Add(Second12);
        Clickable.Add(Third12);
        Clickable.Add(FirstColumn);
        Clickable.Add(SecondColumn);
        Clickable.Add(ThirdColumn);
        Clickable.Add(FirstHalf);
        Clickable.Add(SecondHalf);
        Clickable.Add(Odd);
        Clickable.Add(Even);
        Clickable.Add(Red);
        Clickable.Add(Black);
        Clickable.Add(Bet100);
        Clickable.Add(Bet500);
        Clickable.Add(Bet1000);
        Clickable.Add(HalfBet);
        Clickable.Add(MaxBet);
        Clickable.Add(DropBet);
    }

    private bool TryGetBid(out double bid)
    {
        bid = 0;
        if (double.TryParse(Bid.Text, out double bidCount) && bidCount > 0)
        {
            bid = bidCount;
            return true;
        }
        return false;
    }

    private void ChangeColor(Button sender)
    {
        if (sender == SelectedButton || ((SolidColorBrush)(sender.Background)).Color == SELECTED_COLOR)
        {
            sender.Background = BufferBrush;
            SelectedButton = null;
        }
        else
        {
            if (SelectedButton != null) ChangeColor(SelectedButton);

            BufferBrush = (SolidColorBrush)sender.Background;
            sender.Background = SELECTED_BRUSH;
            SelectedButton = sender;
        }
    }

    private async Task SpinRouletteAsync()
    {
        if (RouletteBids.Count == 0)
        {
            MessageBox.Show("Вы не сделали ставку");
            return;
        }

        foreach (UIElement element in Clickable) element.IsEnabled = false;

        LastBids.Clear();
        foreach (RouletteBid rouletteBid in RouletteBids)
        {
            LastBids.Add(rouletteBid);
        }

        RouletteGameResult? result;
        try
        {
            result = await _rouletteService.Spin(AuthWindow.Instance.CurrentUser.Id, [.. RouletteBids]);
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

        if (result != null)
        {
            if (result.Result)
            {
                Number.Text = "Крутится";

                int winElementId = FindElementId(result.WinElement);

                double duration = 2;
                for (; duration < 4; duration += 0.5)
                {
                    await _rouletteAnimatorService.AnimateElement(RouletteImage, 0, 360, duration);
                }
                await _rouletteAnimatorService.AnimateElement(RouletteImage, 0, ((double)((double)360 / (double)numbers.Length) * (winElementId)), duration);

                Number.Text = $"Выпало число: {result.WinElement}";
                if (result.Win > 0) MessageBox.Show($"Вы выиграли: {result.Win}");
            }
            else MessageBox.Show(result.Message);
        }
        else MessageBox.Show($"Во время игры мы зафиксировали попытку вмешательства в игровой процесс!\nДля честности игра была отменена\nВсе средства отправлены на счёт казино");

    endgame:
        foreach (UIElement element in Clickable) element.IsEnabled = true;
        await AuthWindow.Instance.UpdateProfileInfo();
        Bid.Text = string.Empty;
        RouletteBids.Clear();
        Bids.ItemsSource = RouletteBids;
    }

    private static int FindElementId(int element)
    {
        int winElementId = -1;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] == element)
            {
                winElementId = i;
                break;
            }
        }
        return winElementId;
    }

    private async void Spin_Click(object sender, System.Windows.RoutedEventArgs e) => await SpinRouletteAsync();

    private void Accept_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (TryGetBid(out double bid))
        {
            if (TempBid != null)
            {
                TempBid.BidCount = bid;
                RouletteBid? oldBid = RouletteBids.FirstOrDefault(bid => bid.Type == TempBid.Type);
                if (oldBid != null)
                {
                    oldBid.BidCount += bid;
                }
                else RouletteBids.Add(TempBid);
                Bids.ItemsSource = RouletteBids;
                Bids.Items.Refresh();
            }
            else MessageBox.Show("Выберите, на что ставить");
        }
        else MessageBox.Show("Ставка не корректная!");
    }

    private void LastBid_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        RouletteBids.Clear();
        foreach (RouletteBid rouletteBid in LastBids)
        {
            RouletteBids.Add(rouletteBid);
            Bids.ItemsSource = RouletteBids;
        }
    }

    private void BidClick(object sender, string comment, string bidType)
    {
        if (sender is Button button)
        {
            ChangeColor(button);

            Selected.Text = comment;
            TempBid = new RouletteBid(bidType);
            Bids.UpdateLayout();
        }
        else throw new ArgumentException("sender может быть только кнопкой", nameof(sender));
    }

    private void Number_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            ChangeColor(button);
            if (int.TryParse(button.Content.ToString(), out int number))
            {
                Selected.Text = $"число {number}";
                TempBid = new RouletteBid(number.ToString());
            }
        }
        else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);

    }

    private void DeleteBetButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (button.CommandParameter is RouletteBid bid)
            {
                RouletteBids.Remove(bid);
                Bids.Items.Refresh();
            }
        }
        else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
    }

    private void ThirdColumn_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "третья колонка", ((FrameworkElement)sender).Name);

    private void SecondColumn_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "вторая колонка", ((FrameworkElement)sender).Name);

    private void FirstColumn_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "первая колонка", ((FrameworkElement)sender).Name);

    private void First12_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "первая треть", ((FrameworkElement)sender).Name);

    private void Second12_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "вторая треть", ((FrameworkElement)sender).Name);

    private void Third12_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "третья треть", ((FrameworkElement)sender).Name);

    private void FirstHalf_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "первая половина", ((FrameworkElement)sender).Name);

    private void SecondHalf_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "вторая половина", ((FrameworkElement)sender).Name);

    private void Even_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "чётный", ((FrameworkElement)sender).Name);

    private void Odd_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "нечётный", ((FrameworkElement)sender).Name);

    private void Red_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "красный", ((FrameworkElement)sender).Name);

    private void Black_Click(object sender, System.Windows.RoutedEventArgs e) => BidClick(sender, "чёрный", ((FrameworkElement)sender).Name);

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