using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Core.Models;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Abstractions.Game;
using Gambling.Application.Core.Abstractions.Sessions;
using Gambling.Application.Core.Abstractions.PromtionCodes;
using Gambling.Application.Core.Abstractions.Validation;
using Gambling.Wpf.Admin.Windows.UserDataTableWindows;
using Gambling.Wpf.Admin.WelcomeWindowPages;

namespace Gambling.Wpf.Admin.Windows;

/// <summary>
/// Логика взаимодействия для DataTablesWindow.xaml
/// </summary>
public partial class DataTablesWindow : Window
{
    private readonly IAccountDataService _accountDataService;
    private readonly IGameService _gameService;
    private readonly ISessionStorageService _sessionStorageService;
    private readonly IPromotionalCodeService _promotionalCodeService;
    private readonly IPromocodeValidation _promocodeValidation;

    public DataTablesWindow(
        IAccountDataService accountDataService,
        IGameService gameService,
        ISessionStorageService sessionStorageService,
        IPromotionalCodeService promotionalCodeService,
        IPromocodeValidation promocodeValidation)
    {
        _accountDataService = accountDataService;
        _gameService = gameService;
        _sessionStorageService = sessionStorageService;
        _promotionalCodeService = promotionalCodeService;
        _promocodeValidation = promocodeValidation;

        InitializeComponent();
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        await LoadDataAsync();

        AdminName.Text = AuthWindow.Instance.CurrentAdmin.Name;
    }

    public async Task LoadDataAsync()
    {
        UsersDataGrid.ItemsSource = new ObservableCollection<UserModel>(await _accountDataService.GetAllUsersAsync());
        GamesDataGrid.ItemsSource = new ObservableCollection<GameModel>(await _gameService.GetAllGamesAsync());
        SessionsDataGrid.ItemsSource = new ObservableCollection<SessionModel>(await _sessionStorageService.GetAllSessionsAsync());
        PromocodesDataGrid.ItemsSource = new ObservableCollection<PromotionalCodeModel>(await _promotionalCodeService.GetPromocodeListAsync());
    }

    //Header
    private async void UpdateDataButton_Click(object sender, RoutedEventArgs e)
    {
        await LoadDataAsync();
        MessageBox.Show("Данные обновлены");
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        AuthWindow.Instance.Show();
        Visibility = Visibility.Collapsed;
    }

    //Таблица пользователей
    private void UserStatisticsButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (button.DataContext is UserModel user)
            {
                UserStatisticWindow userStatistic = App.Services.GetRequiredService<UserStatisticWindow>();
                userStatistic.SetUser(user);
                userStatistic.ShowDialog();
            }
        }
        else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
    }

    private async void BanButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (button.DataContext is UserModel user)
            {
                if (!user.IsBlocked)
                {
                    UserBanWindow userBan = App.Services.GetRequiredService<UserBanWindow>();
                    userBan.UserContext = user;
                    userBan.ShowDialog();
                }
                else
                {
                    await _accountDataService.UnblockUser(user.Id);
                    MessageBox.Show("Пользователь разблокирован");
                    await LoadDataAsync();
                }
            }
        }
        else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
    }

    private void ChangeRoleButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (button.DataContext is UserModel user)
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
            if (button.DataContext is UserModel user)
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
            if (button.DataContext is UserModel user)
            {
                PasswordChangeWindow passwordChange = App.Services.GetRequiredService<PasswordChangeWindow>();
                passwordChange.UserContext = user;
                passwordChange.ShowDialog();
            }
        }
        else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
    }

    //Таблица игр
    private void LookPlayerDataButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (button.DataContext is GameModel game)
            {
                UserDataWindow userData = App.Services.GetRequiredService<UserDataWindow>();
                userData.DisplayUserData(game.Player ?? throw new Exception("У игры нет игрока"));
                userData.ShowDialog();
            }
        }
        else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
    }

    //Таблица сессий
    private void LookUserDataButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (button.DataContext is SessionModel session)
            {
                UserDataWindow userData = App.Services.GetRequiredService<UserDataWindow>();
                userData.DisplayUserData(session.User ?? throw new Exception("У сессии нет пользователя"));
                userData.ShowDialog();
            }
        }
        else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
    }

    //Таблица промокодов
    private async void AddPromocode_Click(object sender, RoutedEventArgs e)
    {
        if (_promocodeValidation.Validate(PromocodeText.Text, UsesCountText.Text, IntersentBonusText.Text, QuantivativeBonusText.Text, FreespinsText.Text, out string error))
        {
            await _promotionalCodeService.AddPromocodeAsync(
                PromocodeText.Text,
                int.Parse(UsesCountText.Text),
                int.Parse(IntersentBonusText.Text),
                int.Parse(QuantivativeBonusText.Text),
                int.Parse(FreespinsText.Text)
            );

            await LoadDataAsync();
        }
        else MessageBox.Show(error);
    }

    private void CheckUsesButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (button.DataContext is PromotionalCodeModel promotionalCodeEntity)
            {
                PromocodeActivatesViewerWindow promocodeViewer = App.Services.GetRequiredService<PromocodeActivatesViewerWindow>();
                promocodeViewer.ViewPromocodeActivation(promotionalCodeEntity);
                promocodeViewer.ShowDialog();
            }
        }
        else throw new ArgumentException("sender может быть только кнопкой", ((FrameworkElement)sender).Name);
    }
}