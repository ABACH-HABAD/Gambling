using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Core.Models;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Wpf.Admin.Abstractions;

namespace Gambling.Wpf.Admin.Windows;

/// <summary>
/// Логика взаимодействия для BalanceChangeWindow.xaml
/// </summary>
public partial class BalanceChangeWindow : Window
{
    private const string DefaultTitle = "Изменение баланса";

    private readonly INavigationService _navigationService;
    private readonly IAccountService _accountService;
    private readonly IAccountDataService _accountDataService;
    private readonly ICaptchaService _captchaService;

    internal UserModel? UserContext { get; set; }

    public BalanceChangeWindow(INavigationService navigationService, IAccountService accountService, IAccountDataService accountData, [FromKeyedServices("simple")] ICaptchaService captchaService)
    {
        _navigationService = navigationService;
        _accountService = accountService;
        _accountDataService = accountData;
        _captchaService = captchaService;

        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Height = 300;
        Width = 800;
        Title = UserContext == null ? DefaultTitle : $"{(UserContext.Name ?? UserContext.Email)} - {DefaultTitle}";

        ViewUserBalance();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        if (UserContext == null)
        {
            MessageBox.Show("Такой пользователь не существует");
            return;
        }

        if (!double.TryParse(Balance.Text, out double balance))
        {
            MessageBox.Show("Неверный баланс");
            return;
        }

        if (await _accountDataService.ChangeBalanceAsync(UserContext.Id, balance))
        {
            MessageBox.Show("Баланс изменён");
            DialogResult = true;
            await App.Services.GetRequiredService<DataTablesWindow>().LoadDataAsync();
            Close();
        }
        else
        {
            MessageBox.Show("Ошибка при изменении баланса");
        }
    }

    private void ZeroButton_Click(object sender, RoutedEventArgs e) => SetBalance(0);

    private void MTButton_Click(object sender, RoutedEventArgs e) => AddToBalance(-1000);

    private void MFHButton_Click(object sender, RoutedEventArgs e) => AddToBalance(-500);

    private void MHButton_Click(object sender, RoutedEventArgs e) => AddToBalance(-100);

    private void HButton_Click(object sender, RoutedEventArgs e) => AddToBalance(100);

    private void FHButton_Click(object sender, RoutedEventArgs e) => AddToBalance(500);

    private void TButton_Click(object sender, RoutedEventArgs e) => AddToBalance(1000);

    private void DropButton_Click(object sender, RoutedEventArgs e) => ViewUserBalance();

    private void SetBalance(double sum)
    {
        Balance.Text = sum.ToString();
    }

    private void AddToBalance(double sum)
    {
        if (int.TryParse(Balance.Text, out var balance))
        {
            SetBalance(balance + sum);
        }
        else ViewUserBalance();
    }

    private void ViewUserBalance()
    {
        if (UserContext == null) return;
        SetBalance(UserContext.Balance);
    }
}