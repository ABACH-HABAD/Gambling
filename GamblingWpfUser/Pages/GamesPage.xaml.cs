using System.Windows;
using System.Windows.Controls;
using BusinessLogic.Account.Auth;
using GamblingWpfUser.Navigation;
using GamblingWpfUser.Pages.Games;
using Microsoft.Extensions.DependencyInjection;

namespace GamblingWpfUser.Pages;

/// <summary>
/// Логика взаимодействия для GamesPage.xaml
/// </summary>
public partial class GamesPage : Page
{
    private readonly INavigationService _navigationService;
    private readonly IAccountService _accountService;

    public GamesPage(IAccountService accountService)
    {
        _navigationService = App.Services.GetRequiredService<INavigationService>();
        _navigationService.SetFrame(MainWindow.Instance.MainFrame);
        _accountService = accountService;

        InitializeComponent();
    }


    private void SlotsButton_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo<SlotsPage>();
    }

    private void BlackJackButton_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo<BlackjackPage>();
    }

    private void Roulette_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo<RoulettePage>();
    }
}
