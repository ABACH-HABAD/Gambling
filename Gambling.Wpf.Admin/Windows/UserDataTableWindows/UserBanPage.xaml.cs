using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Core.Models;
using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Wpf.Admin.Abstractions;
using Gambling.Wpf.Admin.Services.CaptchaAdapters;
using Gambling.Wpf.Admin.Windows;

namespace Gambling.Wpf.Admin.WelcomeWindowPages;

/// <summary>
/// Логика взаимодействия для UserBanPage.xaml
/// </summary>
public partial class UserBanWindow : Window
{
    private const string DefaultTitle = "Блокировака пользователя";

    private readonly INavigationService _navigationService;
    private readonly IAccountService _accountService;
    private readonly IAccountDataService _accountDataService;
    private readonly ICaptchaService _captchaService;

    internal UserModel? UserContext { get; set; }

    public UserBanWindow(INavigationService navigationService, IAccountService accountService, IAccountDataService accountDataService, [FromKeyedServices("simple")] ICaptchaService captchaService)
    {
        _navigationService = navigationService;
        _accountService = accountService;
        _accountDataService = accountDataService;
        _captchaService = captchaService;

        InitializeComponent();

        ICaptchaView captchaView = new SimpleCaptchaAdapter(CaptchaPlace);

        _captchaService.InitializationChaptcha(captchaView, 3);
        _captchaService.GenerateChaptcha(5, CaptchaPattern.NumbersAndLetters);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Height = 425;
        Width = 650;
        Title = UserContext == null ? DefaultTitle : $"{(UserContext.Name ?? UserContext.Email)} - {DefaultTitle}";
    }

    private async void Authorization_Click(object sender, RoutedEventArgs e)
    {
        if (UserContext == null)
        {
            MessageBox.Show("Такой пользователь не существует");
            return;
        }

        if (Reason.Text == null || Reason.Text == string.Empty)
        {
            MessageBox.Show("Укажите причину блокировки.");
            return;
        }

        if (_captchaService.AmountOfAttempts <= 0)
        {
            MessageBox.Show("У вас закончились попытки ввести капчу.");
            return;
        }

        if (!_captchaService.CaptchaVerification(CaptchaAnswer.Text))
        {
            MessageBox.Show("Капча введена неверно");
            CaptchaAnswer.Text = string.Empty;
            _captchaService.GenerateChaptcha(5, CaptchaPattern.NumbersAndLetters);
            return;
        }

        if (await _accountDataService.BlockUser(UserContext.Id))
        {
            MessageBox.Show("Пользователь заблокирован");
            DialogResult = true;
            await App.Services.GetRequiredService<DataTablesWindow>().LoadDataAsync();
            Close();
        }
        else
        {
            MessageBox.Show("Неудалось заблокировать пользователя");
        }
    }
}