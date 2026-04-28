using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using GamblingWpfAdmin.CaptchaAdapters;
using GamblingWpfAdmin.Navigation;
using BusinessLogic.Auth;
using BusinessLogic.Captcha;

namespace GamblingWpfAdmin.WelcomeWindowPages;

/// <summary>
/// Логика взаимодействия для AuthPage.xaml
/// </summary>
public partial class AuthPage : Page
{
    private readonly INavigationService _navigationService;
    private readonly IAccountService _accountService;
    private readonly ICaptchaService _captchaService;

    public AuthPage(INavigationService navigationService, IAccountService accountService, [FromKeyedServices("simple")] ICaptchaService captchaService)
    {
        _navigationService = navigationService;
        _accountService = accountService;
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

        LoginResult loginResult = await _accountService.LoginAsync(Login.Text, Password.Password, DeviceType.AdminApp);
        if (loginResult.Result)
        {
            MessageBox.Show("Вы вошли");

            Login.Text = string.Empty;
            Password.Password = string.Empty;

            DataTablesWindow dataTablesWindow = new();
            dataTablesWindow.Show();
            //mainWindow.LoginIntoAdminAccount();
        }
        else MessageBox.Show(loginResult.Message);
    }
}
