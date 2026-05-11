using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.Game;
using DataBaseClasses.Entity;
using GamblingWpfAdmin.WelcomeWindowPages;
using GamblingWpfAdmin.Windows;
using BusinessLogic.Account;

namespace GamblingWpfAdmin
{
    /// <summary>
    /// Логика взаимодействия для DataTablesWindow.xaml
    /// </summary>
    public partial class DataTablesWindow : Window
    {
        private readonly IAccountDataService _accountDataService;
        private readonly IGameService _gameService;
        public DataTablesWindow(IAccountDataService accountDataService, IGameService gameService)
        {
            _accountDataService = accountDataService;
            _gameService = gameService;
            InitializeComponent();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            UsersDataGrid.ItemsSource = await _accountDataService.GetAllUsersAsync(0);
            GamesDataGrid.ItemsSource = await _gameService.GetAllGamesAsync(0);

            /*
            MessageBox.Show("Программа запущена");
            MessageBox.Show("Успешно подключено к сети");
            MessageBox.Show("Подключение не найдено");
            MessageBox.Show("Не удалось найти сервер базы данных");
            MessageBox.Show("Исключение не обработано: [BalanceCannotBeNegativeException]");
            MessageBox.Show("Fatal Error: [0x00067 Your computer will self-destruct]");
            */
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
                if (button.CommandParameter is User user)
                {
                    RoleChangeWindow changeRole = App.Services.GetRequiredService<RoleChangeWindow>();
                    changeRole.UserContext = user;
                    changeRole.ShowDialog();
                }
            }
            else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
        }


        private void AddToBalanceButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.CommandParameter is User user)
                {
                    BalanceChangeWindow balanceChange = App.Services.GetRequiredService<BalanceChangeWindow>();
                    balanceChange.UserContext = user;
                    balanceChange.ShowDialog();
                }
            }
            else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
        }


        private void ClearPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.CommandParameter is User user)
                {
                    PasswordChangeWindow passwordChange = App.Services.GetRequiredService<PasswordChangeWindow>();
                    passwordChange.UserContext = user;
                    passwordChange.ShowDialog();
                }
            }
            else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
        }
    }
}
