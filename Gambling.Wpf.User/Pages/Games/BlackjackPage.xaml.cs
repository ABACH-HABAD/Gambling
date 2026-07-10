using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Gambling.Application.Core.Abstractions.Game.Blackjack;
using Gambling.Application.Core.BusinessModels.GameModels.Blackjack;
using Gambling.Application.Core.Api.Results;
using Gambling.Wpf.User.Windows;
using Gambling.Wpf.User.Classes;

namespace Gambling.Wpf.User.Pages.Games;

/// <summary>
/// Логика взаимодействия для BlackjackPage.xaml
/// </summary>
public partial class BlackjackPage : Page
{
    private ImageBrush ShirtBrush { get; init; }

    private readonly IBlackjackService _blackjackService;
    private readonly IBlackjackScoresService _blackjackScoresService;

    private readonly ObservableCollection<Card> playerCards = [];
    private readonly ObservableCollection<Card> dealerCards = [];

    private BlackjackGameState? CurrentGameState { get; set; }

    private double bet;

    public BlackjackPage(IBlackjackService blackjackService, IBlackjackScoresService blackjackScoresService)
    {
        _blackjackService = blackjackService;
        _blackjackScoresService = blackjackScoresService;

        InitializeComponent();

        EnemyCardCollection.ItemsSource = dealerCards;
        YourCardCollection.ItemsSource = playerCards;

        ShirtBrush = new ImageBrush();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        BlackjackGameState state = await _blackjackService.TryContinueGameAsync(AuthWindow.Instance.CurrentUser.Id);

        bet = state.Bet;

        if (state.IsOk) CurrentGameState = state;
        else CurrentGameState = null;

        UpdateCardsOnTable();
    }

    private void Card_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (ShirtBrush.ImageSource == null) ShirtBrush.ImageSource = BitmapCreator.CreateBitmap(new Uri("pack://application:,,,/Resources/Images/Blackjack/CardShirt.png", UriKind.Absolute));

        if (sender is Border border)
        {
            if (border.DataContext is Card card)
            {
                if (card.IsOpen) border.BorderBrush = card.DisplayColorBrush();
                else
                {
                    border.Background = ShirtBrush;
                }
            }
            else throw new Exception("DataContext does not exist");
        }
        if (sender is TextBlock textBlock)
        {
            if (textBlock.DataContext is Card card)
            {
                textBlock.Foreground = card.DisplayColorBrush();
                if (!card.IsOpen) textBlock.Visibility = System.Windows.Visibility.Hidden;
            }
            else throw new Exception("DataContext does not exist");
        }
    }

    private void UpdateCardsOnTable()
    {
        if (AuthWindow.Instance.CurrentBalance > bet) TakeDouble.IsEnabled = true;
        else TakeDouble.IsEnabled = false;

        if (CurrentGameState != null && CurrentGameState.IsOk)
        {
            playerCards.Clear();
            dealerCards.Clear();

            foreach (Card card in CurrentGameState.PlayerCards) playerCards.Add(card);
            foreach (Card card in CurrentGameState.DealerCards) dealerCards.Add(card);

            BetButtons.Visibility = Visibility.Collapsed;
            MovesButtons.Visibility = Visibility.Visible;

            YourScorce.Text = $"Очки: {_blackjackScoresService.Scores(CurrentGameState.PlayerCards, onlyOpenCards: true)}";
            EnemyScorce.Text = $"Очки: {_blackjackScoresService.Scores(CurrentGameState.DealerCards, onlyOpenCards: true)}";

            CurrentBet.Text = $"Ставка: {bet}";
        }
        else if (CurrentGameState != null)
        {
            MessageBox.Show(CurrentGameState.Message);
            EndGame();
        }
        else
        {
            BetButtons.Visibility = Visibility.Visible;
            MovesButtons.Visibility = Visibility.Collapsed;
        }
    }

    private async void TakeCard_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        BlackjackGameState state = await _blackjackService.TakeCardAsync(AuthWindow.Instance.CurrentUser.Id);
        if (state.IsOk)
        {
            CurrentGameState = state;
            UpdateCardsOnTable();
        }
        else MessageBox.Show(state.Message);
    }

    private async void Pass_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        BlackjackGameState state = await _blackjackService.StandAsync(AuthWindow.Instance.CurrentUser.Id);
        if (state.IsOk)
        {
            CurrentGameState = state;
            UpdateCardsOnTable();
        }
        else
        {
            MessageBox.Show(state.Message);
            return;
        }

        BlackjackGameResult result = await _blackjackService.EndGameAsync(AuthWindow.Instance.CurrentUser.Id);
        MessageBox.Show(
            $"У вас {_blackjackScoresService.Scores(CurrentGameState.PlayerCards)} очков\r\n" +
            $"У противника {_blackjackScoresService.Scores(CurrentGameState.DealerCards, onlyOpenCards: true)} очков\r\n" +
            $"{result.Message}" +
            $"{(result.Win > 0 ? $"\r\nВы выиграли {result.Win}" : "")}");

        EndGame();
    }

    private async void TakeDouble_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        BlackjackGameState state = await _blackjackService.TakeDoubleAsync(AuthWindow.Instance.CurrentUser.Id);
        if (state.IsOk)
        {
            CurrentGameState = state;
            bet *= 2;
            UpdateCardsOnTable();

            await AuthWindow.Instance.UpdateProfileInfo();
        }
        else MessageBox.Show(state.Message);
    }

    private async void EndGame()
    {
        bet = 0;

        BetButtons.Visibility = Visibility.Visible;
        MovesButtons.Visibility = Visibility.Collapsed;

        playerCards.Clear();
        dealerCards.Clear();

        YourScorce.Text = "Очки";
        EnemyScorce.Text = "Очки";

        CurrentBet.Text = string.Empty;

        CurrentGameState = null;

        await AuthWindow.Instance.UpdateProfileInfo();
    }

    private async void Play_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (!double.TryParse(Bid.Text, out double bet))
        {
            MessageBox.Show("Ставка должна быть числом");
            return;
        }

        this.bet = 0;

        BlackjackGameState state = await _blackjackService.FirstMoveAsync(AuthWindow.Instance.CurrentUser.Id, bet);
        if (state.IsOk)
        {
            BetButtons.Visibility = Visibility.Collapsed;
            MovesButtons.Visibility = Visibility.Visible;

            this.bet = bet;

            CurrentGameState = state;
            UpdateCardsOnTable();
        }
        else
        {
            MessageBox.Show(state.Message);
            return;
        }

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