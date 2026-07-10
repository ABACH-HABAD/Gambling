using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Core.Models;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Wpf.Admin.Abstractions;

namespace Gambling.Wpf.Admin.Windows;

/// <summary>
/// Логика взаимодействия для RoleChangeWindow.xaml
/// </summary>
public partial class RoleChangeWindow : Window
{
    private const string DefaultTitle = "Смена роли";

    private readonly INavigationService _navigationService;
    private readonly IAccountService _accountService;
    private readonly IAccountDataService _accountDataService;
    private readonly ICaptchaService _captchaService;

    internal UserModel? UserContext { get; set; }

    public RoleChangeWindow(INavigationService navigationService, IAccountService accountService, IAccountDataService accountDataService, [FromKeyedServices("simple")] ICaptchaService captchaService)
    {
        _navigationService = navigationService;
        _accountDataService = accountDataService;
        _accountService = accountService;
        _captchaService = captchaService;

        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Height = 225;
        Width = 475;
        Title = UserContext == null ? DefaultTitle : $"{(UserContext.Name ?? UserContext.Email)} - {DefaultTitle}";
    }

    private async void AcceptButton_Click(object sender, RoutedEventArgs e)
    {
        if (UserContext == null)
        {
            MessageBox.Show("Такой пользователь не существует");
            return;
        }

        if (Role.SelectionBoxItem == null)
        {
            MessageBox.Show("Укажите роль");
            return;
        }

        if (await _accountDataService.ChangeStatusAsync(UserContext.Id, Role.SelectedIndex + 1))
        {
            MessageBox.Show("Роль успешно изменена!");
            DialogResult = true;
            await App.Services.GetRequiredService<DataTablesWindow>().LoadDataAsync();
            Close();
        }
        else
        {
            MessageBox.Show("Неудалось изменить роль");
        }
    }
}