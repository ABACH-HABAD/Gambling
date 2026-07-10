using System.Windows;
using Gambling.Core.Models;
using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Statistics;
using Gambling.Application.Core.Api.Results;

namespace Gambling.Wpf.Admin.Windows.UserDataTableWindows;

/// <summary>
/// Логика взаимодействия для UserStatisticWindow.xaml
/// </summary>
public partial class UserStatisticWindow : Window
{
    private readonly IUserStatisticsService _statisticsService;
    private UserModel? _user;
    public UserStatisticWindow(IUserStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
        InitializeComponent();

        All.IsChecked = true;
    }

    public async void SetUser(UserModel userEntity)
    {
        _user = userEntity;
        Title = "Статистика игрока " + _user.Name;
        await DisplayStatisticAsync(GameType.Any);
    }

    private async Task DisplayStatisticAsync(GameType type)
    {
        if (_user == null) return;

        UserStatisticResult statisticResult;

        try
        {
            statisticResult = await _statisticsService.GetUserStatisticResultAsync(_user.Id, type);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return;
        }

        WinFrequency.Text = $"% побед {(int)(Math.Round(statisticResult.WinFrequency, 2) * 100)}%";
        TotalBalance.Text = $"{statisticResult.TotalBalance}";
        WinCount.Text = $"{statisticResult.WinCount}";
        WinBalance.Text = $"{statisticResult.WinBalance}";
        LossCount.Text = $"{statisticResult.LossCount}";
        LossBalance.Text = $"{statisticResult.LossBalance}";
    }

    private async void All_Checked(object sender, RoutedEventArgs e) => await DisplayStatisticAsync(GameType.Any);

    private async void Slots_Checked(object sender, RoutedEventArgs e) => await DisplayStatisticAsync(GameType.Slots);

    private async void Blackjack_Checked(object sender, RoutedEventArgs e) => await DisplayStatisticAsync(GameType.Blackjack);

    private async void Roulette_Checked(object sender, RoutedEventArgs e) => await DisplayStatisticAsync(GameType.Roulette);
}