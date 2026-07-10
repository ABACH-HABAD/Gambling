using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Core.Models;
using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Validation;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Wpf.User.Abstractions;
using Gambling.Wpf.User.Services.CaptchaAdapters;

namespace Gambling.Wpf.User.Windows;

/// <summary>
/// Логика взаимодействия для PasswordChangeWindow.xaml
/// </summary>
public partial class PasswordChangeWindow : Window
{
    private const string DefaultTitle = "Смена пароля";

    private readonly ITwoPasswordsValidation _twoPasswordsValidation;
    private readonly INavigationService _navigationService;
    private readonly IAccountService _accountService;
    private readonly ICaptchaService _captchaService;

    internal UserModel? UserContext { get; set; }

    public PasswordChangeWindow(ITwoPasswordsValidation twoPasswordsValidation, INavigationService navigationService, IAccountService accountService, [FromKeyedServices("easy")] ICaptchaService captchaService)
    {
        _twoPasswordsValidation = twoPasswordsValidation;
        _navigationService = navigationService;
        _accountService = accountService;
        _captchaService = captchaService;

        InitializeComponent();

        ICaptchaView captchaView = new EasyCaptchaAdapter(CaptchaPlace);

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
        if (!_twoPasswordsValidation.Validate(Password1.Password, out string error1))
        {
            MessageBox.Show(error1);
            return;
        }

        if (!_twoPasswordsValidation.ValidateTwoPasswords(Password2.Password, Password3.Password, out string error2))
        {
            MessageBox.Show(error2);
            return;
        }

        try
        {
            if (await _accountService.ChangePasswordAsync(0, Password1.Password, Password2.Password, Password3.Password))
            {
                MessageBox.Show("Пароль успешно изменён");
            }
            else MessageBox.Show("Пароль не изменён");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Пароль не изменён: {ex.Message}");
        }
    }
}