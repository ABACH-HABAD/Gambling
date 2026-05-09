using BusinessLogic.Auth;
using BusinessLogic.Game;
using DataBaseClasses.Entity;
using GamblingWpfAdmin.WelcomeWindowPages;
using GamblingWpfAdmin.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace GamblingWpfAdmin
{
    /// <summary>
    /// Логика взаимодействия для DataTablesWindow.xaml
    /// </summary>
    public partial class DataTablesWindow : Window
    {
        private readonly IAccountService _accountService;
        private readonly IGameService _gameService;
        public DataTablesWindow(IAccountService accountService, IGameService gameService)
        {
            _accountService = accountService;
            _gameService = gameService;
            InitializeComponent();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            UsersDataGrid.ItemsSource = await _accountService.GetAllUsersAsync(0);
            GamesDataGrid.ItemsSource = await _gameService.GetAllGamesAsync(0);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.CommandParameter is User)
                {
                    //App.Services.GetRequiredService<Ed>
                }
            }
            else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
        }

        private void BanButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.CommandParameter is User user)
                {
                    UserBanWindow userBan = App.Services.GetRequiredService<UserBanWindow>();
                    userBan.UserContext = user;
                    userBan.ShowDialog();
                }
            }
            else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
        }


        private void ChangeRoleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.CommandParameter is User)
                {
                    RoleChangeWindow userBan = App.Services.GetRequiredService<RoleChangeWindow>();
                    userBan.ShowDialog();
                }
            }
            else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
        }


        private void AddToBalanceButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.CommandParameter is User)
                {
                    BalanceChangeWindow balanceChange = App.Services.GetRequiredService<BalanceChangeWindow>();
                    balanceChange.ShowDialog();
                }
            }
            else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
        }


        private void ClearPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.CommandParameter is User)
                {
                    PasswordChangeWindow userBan = App.Services.GetRequiredService<PasswordChangeWindow>();
                    userBan.ShowDialog();
                }
            }
            else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
        }
    }
}
