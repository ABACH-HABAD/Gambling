using BusinessLogic.Game.Blackjack;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GamblingWpfUser.Pages.Games;

/// <summary>
/// Логика взаимодействия для BlackjackPage.xaml
/// </summary>
public partial class BlackjackPage : Page
{
    private ImageBrush ShirtBrush { get; init; } = new ImageBrush(new BitmapImage(new Uri("/Resources/Images/Blackjack/CardShirt.png")));

    private readonly IBlackjackService _blackjackService;

    private readonly ObservableCollection<Card> playerCards = [];
    private readonly ObservableCollection<Card> dealerCards = [];

    public BlackjackPage(IBlackjackService blackjackService)
    {
        _blackjackService = blackjackService;

        InitializeComponent();

        EnemyCardCollection.ItemsSource = dealerCards;
        YourCardCollection.ItemsSource = playerCards;
    }

    private void Card_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (sender is Border border)
        {
            if (border.DataContext is Card card)
            {
                if (card.IsOpen) border.BorderBrush = card.DisplayColorBrush();
                else border.BorderBrush = ShirtBrush;
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

    private void TakeCard_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        _blackjackService.TakeCard(0);
    }

    private void Pass_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        _blackjackService.Stand(0);
    }

    private void TakeDouble_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        _blackjackService.TakeDouble(0);
    }

    private void Play_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (!double.TryParse(Bid.Text, out double bet))
        {
            MessageBox.Show("Ставка должна быть числом");
            return;
        }

        BetButtons.Visibility = Visibility.Collapsed;
        MovesButtons.Visibility = Visibility.Visible;

        _blackjackService.FirstMove(0, bet);
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

    private void HalfBet_Click(object sender, RoutedEventArgs e) => SetBet(Math.Round(MainWindow.Instance.CurrentBalance / 2));

    private void MaxBet_Click(object sender, RoutedEventArgs e) => SetBet(MainWindow.Instance.CurrentBalance);

    private void DropBet_Click(object sender, RoutedEventArgs e) => SetBet(0);
}
