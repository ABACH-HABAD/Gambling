using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using DataBaseClasses.Entity;
using BusinessLogic.Account.Auth;
using BusinessLogic.Validation;
using BusinessLogic.Captcha;
using GamblingWpfUser.CaptchaAdapters;
using GamblingWpfUser.Navigation;

namespace GamblingWpfUser.Windows;

/// <summary>
/// Логика взаимодействия для EmailChangeWindow.xaml
/// </summary>
public partial class EmailChangeWindow : Window
{
    private const string DEFAULT_TITLE = "Смена пароля";

    private readonly IEmailValidation _emailValidation;
    private readonly INavigationService _navigationService;
    private readonly IAccountService _accountService;
    private readonly ICaptchaService _captchaService;

    internal User? UserContext { get; set; }

    public EmailChangeWindow(IEmailValidation emailValidation, INavigationService navigationService, IAccountService accountService, [FromKeyedServices("easy")] ICaptchaService captchaService)
    {
        _emailValidation = emailValidation;
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
        Title = UserContext == null ? DEFAULT_TITLE : $"{(UserContext.Name ?? UserContext.Email)} - {DEFAULT_TITLE}";
    }

    private async void Authorization_Click(object sender, RoutedEventArgs e)
    {
        if (!_emailValidation.Validate(OldEmail.Text, out string error1))
        {
            MessageBox.Show(error1);
            return;
        }

        if (!_emailValidation.Validate(NewEmail.Text, out string error2))
        {
            MessageBox.Show(error2);
            return;
        }

        try
        {
            if (await _accountService.ChangeEmailAsync(0, OldEmail.Text, NewEmail.Text))
            {
                MessageBox.Show("Почта успешно изменена");
            }
            else MessageBox.Show("Почта не изменена");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Почта не изменена: {ex.Message}");
        }
    }
}
