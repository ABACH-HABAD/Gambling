using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Api.Results;
using Gambling.Wpf.User.Abstractions;
using Gambling.Wpf.User.Services.CaptchaAdapters;
using Gambling.Wpf.User.Windows;

namespace Gambling.Wpf.User.Pages;

/// <summary>
/// Логика взаимодействия для AuthPage.xaml
/// </summary>
public partial class AuthPage : Page
{
    private readonly INavigationService _navigationService;
    private readonly IAccountService _accountService;
    private readonly IAccountDataService _accountDataService;
    private readonly ICaptchaService _captchaService;

    public AuthPage(INavigationService navigationService, IAccountService accountService, IAccountDataService dataService, [FromKeyedServices("simple")] ICaptchaService captchaService)
    {
        _navigationService = navigationService;
        _navigationService.SetFrame(AuthWindow.Instance.MainFrame);
        _accountService = accountService;
        _accountDataService = dataService;
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

        LoginResult result = await _accountService.LoginAsync(Login.Text, Password.Password, DeviceType.Windows);
        if (result.Result)
        {
            MessageBox.Show("Вы вошли");
            AuthWindow.Instance.CurrentUser = await _accountDataService.GetUserDataAsync(0) ?? throw new Exception("Данные пользователя загрузить не удалось");

            Login.Text = string.Empty;
            Password.Password = string.Empty;

            _navigationService.NavigateTo<GamesPage>();
        }
        else MessageBox.Show(result.Message);
    }
}
