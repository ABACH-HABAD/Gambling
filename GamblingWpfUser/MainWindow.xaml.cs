using BusinessLogic.Account;
using BusinessLogic.Account.Auth;
using BusinessLogic.Token;
using DataBaseClasses.Entity;
using GamblingWpfUser.Navigation;
using GamblingWpfUser.Pages;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GamblingWpfUser;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public sealed partial class MainWindow : Window
{
    public const string IS_NAME = "Оналйн казино «Игровой Центр»";

    private INavigationService _navigationService = null!;
    private IAccountService _accountService = null!;
    private ITokenStorageService _tokenStorageService = null!;

    internal double CurrentBalance { get; private set; }

    public static MainWindow Instance { get; private set; } = null!;

    public MainWindow()
    {
        Instance = this;
        Title = IS_NAME;

        InitializeComponent();
    }

    public void ChangeTitle(string title) => Title = $"{IS_NAME} - {title}";
    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        _navigationService = App.Services.GetRequiredService<INavigationService>();
        _navigationService.SetFrame(MainFrame);

        _accountService = App.Services.GetRequiredService<IAccountService>();
        _tokenStorageService = App.Services.GetRequiredKeyedService<ITokenStorageService>("refresh");
        string? refreshToken = await _tokenStorageService.GetTokenAsync();
        if (refreshToken != null)
        {
            LoginResult result = await _accountService.AutoLoginAsync(refreshToken, BusinessLogic.Account.Auth.DeviceType.Windows, null!);

            if (result.Result) _navigationService.NavigateTo<GamesPage>();
            else _navigationService.NavigateTo<AuthPage>();
        }
        else _navigationService.NavigateTo<AuthPage>();
    }

    private void OnClosed(object sender, EventArgs e)
    {
        Application.Current.Shutdown();
    }

    public async Task UpdateProfileInfo()
    {
        if (_navigationService.CurrentPageType == typeof(AuthPage) || _navigationService.CurrentPageType == typeof(RegistrationPage)) ProfileInfo.Visibility = Visibility.Hidden;
        else
        {
            ProfileInfo.Visibility = Visibility.Visible;
            IAccountDataService accountDataService = App.Services.GetRequiredService<IAccountDataService>();
            User? user = await accountDataService.GetUserDataAsync(0);
            if (user != null)
            {
                if (user.Name == null)
                {
                    user.Name = $"ИгрокВКазино{user.Id}";
                    await accountDataService.UpdateUserDataAsync(user);
                }
                user.Balance ??= 0;
                CurrentBalance = user.Balance ?? 0;
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
        await _accountService.LogoutAsync(string.Empty, BusinessLogic.Account.Auth.DeviceType.Windows, null);
        _navigationService.NavigateTo<AuthPage>();
    }

    private async void Logout_Click(object sender, RoutedEventArgs e) => await Logout();

    private void ProfileButton_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo<ProfilePage>();
    }
}