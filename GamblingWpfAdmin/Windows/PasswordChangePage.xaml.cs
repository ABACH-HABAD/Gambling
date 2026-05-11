using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using BusinessLogic.Captcha;
using GamblingWpfAdmin.CaptchaAdapters;
using GamblingWpfAdmin.Navigation;
using DataBaseClasses.Entity;
using BusinessLogic.Account.Auth;

namespace GamblingWpfAdmin.WelcomeWindowPages;

/// <summary>
/// Логика взаимодействия для PasswordChangePage.xaml
/// </summary>
public partial class PasswordChangeWindow : Window
{
    private const string DEFAULT_TITLE = "Смена пароля";

    private readonly INavigationService _navigationService;
    private readonly IAccountService _accountService;
    private readonly ICaptchaService _captchaService;

    internal User? UserContext { get; set; }

    public PasswordChangeWindow(INavigationService navigationService, IAccountService accountService, [FromKeyedServices("simple")] ICaptchaService captchaService)
    {
        _navigationService = navigationService;
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
        Title = UserContext == null ? DEFAULT_TITLE : $"{(UserContext.Name ?? UserContext.Email)} - {DEFAULT_TITLE}";
    }
}
