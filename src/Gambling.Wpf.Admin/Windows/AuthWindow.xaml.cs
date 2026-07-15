using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Core.Models;
using Gambling.Wpf.Admin.WelcomeWindowPages;
using Gambling.Wpf.Admin.Abstractions;

namespace Gambling.Wpf.Admin.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class AuthWindow : Window
{
    public const string InformationSystemName = "ИС Азартные игры (для администраторов)"; //ИС Азартные игры (для администраторов)

    public static AuthWindow Instance { get; private set; } = null!;

    public UserModel CurrentAdmin = null!;

    private INavigationService _navigationService = null!;

    public AuthWindow()
    {
        Instance = this;
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _navigationService = App.Services.GetRequiredService<INavigationService>();
        _navigationService.SetFrame(MainFrame);

        NavigateToAuthorizationPage();
    }

    public void NavigateToAuthorizationPage()
    {
        Title = $"{InformationSystemName} - Авторизация";
        _navigationService.NavigateTo<AuthPage>();
    }

    public void LoginIntoAdminAccount()
    {
        DataTablesWindow dataTablesWindow = App.Services.GetRequiredService<DataTablesWindow>();
        dataTablesWindow.Show();
        dataTablesWindow.Visibility = Visibility.Visible;
        Visibility = Visibility.Collapsed;
        NavigateToAuthorizationPage();
    }
}