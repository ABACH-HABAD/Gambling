using BusinessLogic.Account;
using BusinessLogic.Account.Profile.Statistics;
using BusinessLogic.Game;
using BusinessLogic.Validation;
using GamblingWpfUser.Windows;
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
        private readonly IAccountDataService _accountDataService;
        private readonly IUserStatisticsService _statisticsService;

        public ProfilePage(IAccountDataService accountService, INameValidation nameValidation, IUserStatisticsService statisticsService)
        {
            _nameValidation = nameValidation;
            _accountDataService = accountService;
            _statisticsService = statisticsService;

            InitializeComponent();

            UserName.Text = MainWindow.Instance.UserName.Text;
            ChangeUserName.Text = MainWindow.Instance.UserName.Text;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Balance.Text = MainWindow.Instance.CurrentBalance.ToString();
            UserName.Text = MainWindow.Instance.UserName.Text;
            ChangeUserName.Text = MainWindow.Instance.UserName.Text;
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

            await _accountDataService.UpdateUserDataAsync(new DataBaseClasses.Entity.User() { Name = ChangeUserName.Text });
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

            UserStatisticResult statisticResult;

            try
            {
                statisticResult = await _statisticsService.GetUserStatisticResultAsync(0, type);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            WinFrequency.Text = $"% побед {(int)(Math.Round(statisticResult.WinFrequency, 2) * 100)}%";
            TotalBalance.Text = $"{statisticResult.TotalBalance}";
            WinCount.Text = $"{statisticResult.WinCount}";
            WinBalance.Text = $"{statisticResult.WinBalance}";
            LossCount.Text = $"{statisticResult.LossCount}";
            LossBalance.Text = $"{statisticResult.LossBalance}";
        }

        private async void All_Checked(object sender, RoutedEventArgs e) => await DisplayStatistic(GameType.Any);

        private async void Slots_Checked(object sender, RoutedEventArgs e) => await DisplayStatistic(GameType.Slots);

        private async void Blackjack_Checked(object sender, RoutedEventArgs e) => await DisplayStatistic(GameType.Blackjack);

        private async void Roulette_Checked(object sender, RoutedEventArgs e) => await DisplayStatistic(GameType.Roulette);

        private void ChangeEmail_Click(object sender, RoutedEventArgs e)
        {
            EmailChangeWindow window = App.Services.GetRequiredService<EmailChangeWindow>();
            window.ShowDialog();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordChangeWindow window = App.Services.GetRequiredService<PasswordChangeWindow>();
            window.ShowDialog();
        }
    }
}
