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

    public static MainWindow Instance { get; private set; } = null!;

    private INavigationService _navigationService = null!;

    public MainWindow()
    {
        Instance = this;
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _navigationService = App.Services.GetRequiredService<INavigationService>();
        _navigationService.SetFrame(MainFrame);

        NavigateToAuthorizationPage();
        //NavigateToChangePasswordPage();
        //NavigateToUserBanPage();
    }

    public void NavigateToAuthorizationPage()
    {
        Title = $"{IS_NAME} - Авторизация";
        _navigationService.NavigateTo<AuthPage>();
    }

    public void LoginIntoAdminAccount()
    {
        DataTablesWindow dataTablesWindow = App.Services.GetRequiredService<DataTablesWindow>();
        dataTablesWindow.Show();
        this.Close();
    }
}