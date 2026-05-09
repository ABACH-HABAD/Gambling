using BusinessLogic.Auth;
using BusinessLogic.Game;
using BusinessLogic.Profile.Statistics;
using BusinessLogic.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace GamblingWpfUser.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private readonly INameValidation _nameValidation;
        private readonly IAccountService _accountService;
        private readonly IUserStatisticsService _statisticsService;

        public ProfilePage(IAccountService accountService, INameValidation nameValidation, IUserStatisticsService statisticsService)
        {
            _nameValidation = nameValidation;
            _accountService = accountService;
            _statisticsService = statisticsService;

            InitializeComponent();

            UserName.Text = MainWindow.Instance.UserName.Text;
            ChangeUserName.Text = MainWindow.Instance.UserName.Text;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Balance.Text = MainWindow.Instance.CurrentBalance.ToString();
            await DisplayStatistic(GameType.Any);
            All.IsChecked = true;
        }

        private void ChangeName_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeUserName.Text = MainWindow.Instance.UserName.Text;
            UserNamePanel.Visibility = System.Windows.Visibility.Collapsed;
            ChangeUserNamePanel.Visibility = System.Windows.Visibility.Visible;
        }

        private async void ConfirmChangeName_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_nameValidation.Validate(ChangeUserName.Text, out string error))
            {
                MessageBox.Show(error);
                return;
            }

            await _accountService.UpdateUserDataAsync(new DataBaseClasses.Entity.User() { Name = ChangeUserName.Text });
            await MainWindow.Instance.UpdateProfileInfo();
            UserName.Text = MainWindow.Instance.UserName.Text;
            ChangeUserName.Text = MainWindow.Instance.UserName.Text;
            UserNamePanel.Visibility = System.Windows.Visibility.Visible;
            ChangeUserNamePanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void DenyChangeName_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeUserName.Text = MainWindow.Instance.UserName.Text;
            UserNamePanel.Visibility = System.Windows.Visibility.Visible;
            ChangeUserNamePanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void AddToBalance_Click(object sender, RoutedEventArgs e)
        {
            PayWindow payWindow = App.Services.GetRequiredService<PayWindow>();
            payWindow.ShowDialog();
        }

        private void WhithdrawFromBalance_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данная опция пока не доступна");
        }

        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            await MainWindow.Instance.Logout();   
        }

        private async void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            await MainWindow.Instance.Logout();
        }

        private async Task DisplayStatistic(GameType type)
        {
            Balance.Text = MainWindow.Instance.CurrentBalance.ToString();

            WinFrequency.Text = $"% побед {(int)(Math.Round(await _statisticsService.WinFrequency(0, type), 2) * 100)}%";
            TotalBalance.Text = $"{await _statisticsService.TotalBalance(0, type)}";
            WinCount.Text = $"{await _statisticsService.WinCount(0, type)}";
            WinBalance.Text = $"{await _statisticsService.WinBalance(0, type)}";
            LossCount.Text = $"{await _statisticsService.LossCount(0, type)}";
            LossBalance.Text = $"{await _statisticsService.LossBalance(0, type)}";
        }

        private async void All_Checked(object sender, RoutedEventArgs e) => await DisplayStatistic(GameType.Any);

        private async void Slots_Checked(object sender, RoutedEventArgs e) => await DisplayStatistic(GameType.Slots);

        private async void Blackjack_Checked(object sender, RoutedEventArgs e) => await DisplayStatistic(GameType.Blackjack);

        private async void Roulette_Checked(object sender, RoutedEventArgs e) => await DisplayStatistic(GameType.Roulette);
    }
}
