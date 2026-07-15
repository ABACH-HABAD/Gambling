using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Wpf.User.Pages;
using Gambling.Wpf.User.Abstractions;
using Gambling.Core.Models;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Token;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Profile;

namespace Gambling.Wpf.User.Windows;

/// <summary>
/// Interaction logic for AuthWindow.xaml
/// </summary>
public partial class AuthWindow : Window
{
    public const string InformationSystemName = "Оналйн казино «Игровой Центр»";

    private UserModel _currentUser = null!;

    private INavigationService _navigationService = null!;
    private IAccountService _accountService = null!;
    private ITokenStorageService _tokenStorageService = null!;

    internal double CurrentBalance { get; private set; }

    internal UserModel CurrentUser
    {
        get => _currentUser;
        set
        {
            if (_currentUser == null) _currentUser = value;
            else throw new InvalidOperationException("Нельзя повторно установить значение пользователя");
        }
    }

    public static AuthWindow Instance { get; private set; } = null!;

    public AuthWindow()
    {
        Instance = this;
        Title = InformationSystemName;

        InitializeComponent();
    }

    public void ChangeTitle(string title) => Title = $"{InformationSystemName} - {title}";
    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        _navigationService = App.Services.GetRequiredService<INavigationService>();
        _navigationService.SetFrame(MainFrame);

        _accountService = App.Services.GetRequiredService<IAccountService>();
        _tokenStorageService = App.Services.GetRequiredKeyedService<ITokenStorageService>("refresh");
        string? refreshToken = await _tokenStorageService.GetTokenAsync();
        if (refreshToken != null)
        {
            LoginResult result = await _accountService.AutoLoginAsync(refreshToken, DeviceType.Windows, null!);

            if (result.Result) _navigationService.NavigateTo<GamesPage>();
            else _navigationService.NavigateTo<AuthPage>();
        }
        else _navigationService.NavigateTo<AuthPage>();
    }

    private void OnClosed(object sender, EventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
    }

    public async Task UpdateProfileInfo()
    {
        if (_navigationService.CurrentPageType == typeof(AuthPage) || _navigationService.CurrentPageType == typeof(RegistrationPage)) ProfileInfo.Visibility = Visibility.Hidden;
        else
        {
            ProfileInfo.Visibility = Visibility.Visible;
            IAccountDataService accountDataService = App.Services.GetRequiredService<IAccountDataService>();
            UserModel? user = await accountDataService.GetUserDataAsync(AuthWindow.Instance.CurrentUser != null ? AuthWindow.Instance.CurrentUser.Id : 0);
            if (user != null)
            {
                _currentUser = user;
                if (user.Name == null)
                {
                    user.Name = $"ИгрокВКазино{user.Id}";
                    await accountDataService.ChangeNameAsync(user.Id, $"ИгрокВКазино{user.Id}");
                }
                CurrentBalance = user.Balance;
                UserName.Text = user.Name;
                Balance.Text = $"Баланс: {CurrentBalance}";
            }
        }
    }

    private void Back_Click(object sender, RoutedEventArgs e) => _navigationService.NavigateTo<GamesPage>();

    private async void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e) => await UpdateProfileInfo();

    private void AddToBalance_Click(object sender, RoutedEventArgs e)
    {
        PayWindow payWindow = App.Services.GetRequiredService<PayWindow>();
        payWindow.ShowDialog();
    }

    public async Task Logout()
    {
        _currentUser = null!;
        await _accountService.LogoutAsync(string.Empty, DeviceType.Windows, null);
        _navigationService.NavigateTo<AuthPage>();
    }

    private async void Logout_Click(object sender, RoutedEventArgs e) => await Logout();

    private void ProfileButton_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo<ProfilePage>();
    }
}