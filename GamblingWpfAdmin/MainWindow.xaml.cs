using GamblingWpfAdmin.Navigation;
using GamblingWpfAdmin.WelcomeWindowPages;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GamblingWpfAdmin;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public const string IS_NAME = "Информационная система"; //ИС Азартные игры (для администраторов)

    private INavigationService _navigationService = null!;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _navigationService = App.Services.GetRequiredService<INavigationService>();
        _navigationService.SetFrame(MainFrame);

        NavigateToAuthorizationPage();
        //NavigateToChangePasswordPage();
        //NavigateToUserBanPage();

        MilkProduction milkProduction = new();
        milkProduction.Show();
    }

    private void OnClosed(object sender, EventArgs e)
    {
        Application.Current.Shutdown();
    }

    public void NavigateToAuthorizationPage()
    {
        Title = $"{IS_NAME} - Авторизация";
        _navigationService.NavigateTo<AuthPage>();
    }

    public void NavigateToChangePasswordPage()
    {
        Title = $"{IS_NAME} - Смена пароля";
        _navigationService.NavigateTo<PasswordChangePage>();
    }

    public void NavigateToUserBanPage()
    {
        Title = $"{IS_NAME} - Блокировка пользователя";
        _navigationService.NavigateTo<UserBanPage>();
    }

    public void LoginIntoAdminAccount()
    {
        DataTablesWindow dataTablesWindow = new();
        dataTablesWindow.Show();
        Close();
    }

}