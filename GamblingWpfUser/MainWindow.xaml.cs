using BusinessLogic.Auth;
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
    public const string IS_NAME = "Оналйн казино NAEBALOVO.NET";

    private INavigationService _navigationService = null!;
    private ILoginChecker _loginChecker = null!;

    public static MainWindow Instance { get; private set; } = null!;

    public MainWindow()
    {
        Instance = this;

        InitializeComponent();
    }

    public void ChangeTitle(string title) => Title = $"{IS_NAME} - {title}";
    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        _navigationService = App.Services.GetRequiredService<INavigationService>();
        _navigationService.SetFrame(MainFrame);

        _loginChecker = App.Services.GetRequiredService<ILoginChecker>();
        LoginResult result = await _loginChecker.CheckActiveLoginAsync(null!, 1, null!);
        if (result.Result)
        {
            _navigationService.NavigateTo<GamesPage>();
        }
        else _navigationService.NavigateTo<AuthPage>();
    }

    private void OnClosed(object sender, EventArgs e)
    {
        Application.Current.Shutdown();
    }

    public async void UpdateProfileInfo()
    {
        if (_navigationService.CurrentPageType == typeof(AuthPage) || _navigationService.CurrentPageType == typeof(RegistrationPage)) ProfileInfo.Visibility = Visibility.Hidden;
        else
        {
            ProfileInfo.Visibility = Visibility.Visible;
            IAccountService accountService = App.Services.GetRequiredService<IAccountService>();
            User? user = await accountService.GetUserData(0);
            if (user != null)
            {
                if (user.Name == null)
                {
                    user.Name = $"ИгрокВКазино{user.Id}";
                    await accountService.UpdateUserDataAsync(user);
                }
                user.Balance ??= 0;
                UserName.Text = user.Name;
                Balance.Text = $"Баланс: {user.Balance}";
            }
        }
    }

    private void Back_Click(object sender, RoutedEventArgs e) => _navigationService.NavigateTo<GamesPage>();

    private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e) => UpdateProfileInfo();
}