using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Api.Results;
using Gambling.Wpf.Admin.Abstractions;
using Gambling.Wpf.Admin.Services.CaptchaAdapters;
using Gambling.Wpf.Admin.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace Gambling.Wpf.Admin.WelcomeWindowPages;

/// <summary>
/// Логика взаимодействия для AuthPage.xaml
/// </summary>
public partial class AuthPage : Page
{
    private readonly INavigationService _navigationService;
    private readonly IAccountService _accountService;
    private readonly IAccountDataService _accountDataService;
    private readonly ICaptchaService _captchaService;

    public AuthPage(INavigationService navigationService, IAccountService accountService, IAccountDataService accountDataService, [FromKeyedServices("simple")] ICaptchaService captchaService)
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

    private void Registration_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo<RegistrationPage>();
    }

    private async void Authorization_Click(object sender, RoutedEventArgs e)
    {
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

        LoginResult loginResult = await _accountService.LoginAsync(Login.Text, Password.Password, DeviceType.AdminApp, loginAsAdmin: true);
        if (loginResult.Result)
        {
            MessageBox.Show("Вы вошли");

            Login.Text = string.Empty;
            Password.Password = string.Empty;
            CaptchaAnswer.Text = string.Empty;

            _captchaService.GenerateChaptcha(5, CaptchaPattern.NumbersAndLetters);

            AuthWindow.Instance.CurrentAdmin = await _accountDataService.GetUserDataAsync(0) ?? throw new Exception("Данные администратора загрузить не удалось");

            AuthWindow mainWindow = AuthWindow.Instance;
            mainWindow.LoginIntoAdminAccount();
        }
        else MessageBox.Show(loginResult.Message);
    }
}