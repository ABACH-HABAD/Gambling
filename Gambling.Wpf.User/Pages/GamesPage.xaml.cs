using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Wpf.User.Abstractions;
using Gambling.Wpf.User.Windows;
using Gambling.Wpf.User.Pages.Games;

namespace Gambling.Wpf.User.Pages;

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
        _navigationService.SetFrame(AuthWindow.Instance.MainFrame);
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