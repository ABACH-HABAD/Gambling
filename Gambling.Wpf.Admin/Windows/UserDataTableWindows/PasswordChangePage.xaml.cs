using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Core.Models;
using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Wpf.Admin.Abstractions;
using Gambling.Wpf.Admin.Services.CaptchaAdapters;

namespace Gambling.Wpf.Admin.WelcomeWindowPages;

/// <summary>
/// Логика взаимодействия для PasswordChangePage.xaml
/// </summary>
public partial class PasswordChangeWindow : Window
{
    private const string DefaultTitle = "Смена пароля";

    private readonly INavigationService _navigationService;
    private readonly IAccountDataService _accountDataService;
    private readonly IAccountService _accountService;
    private readonly ICaptchaService _captchaService;

    internal UserModel? UserContext { get; set; }

    public PasswordChangeWindow(INavigationService navigationService, IAccountService accountService, IAccountDataService accountDataService, [FromKeyedServices("simple")] ICaptchaService captchaService)
    {
        _navigationService = navigationService;
        _accountDataService = accountDataService;
        _accountService = accountService;
        _captchaService = captchaService;

        InitializeComponent();

        ICaptchaView captchaView = new SimpleCaptchaAdapter(CaptchaPlace);

        _captchaService.InitializationChaptcha(captchaView, 3);
        _captchaService.GenerateChaptcha(5, CaptchaPattern.NumbersAndLetters);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Height = 425;
        Width = 700;
        Title = UserContext == null ? DefaultTitle : $"{(UserContext.Name ?? UserContext.Email)} - {DefaultTitle}";
    }

    private async void Authorization_Click(object sender, RoutedEventArgs e)
    {
        if (UserContext == null)
        {
            MessageBox.Show("Такой пользователь не существует");
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

        if (await _accountService.ChangePasswordAsync(UserContext.Id, string.Empty, Password2.Password, Password3.Password, forceChange: true))
        {
            MessageBox.Show("Пароль успешно изменён");
            DialogResult = true;
            Close();
        }
        else
        {
            MessageBox.Show("Ошибка при сохранении пароля");
        }
    }
}