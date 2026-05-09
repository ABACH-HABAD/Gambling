using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using BusinessLogic.Auth;
using BusinessLogic.Captcha;
using GamblingWpfAdmin.CaptchaAdapters;
using GamblingWpfAdmin.Navigation;

namespace GamblingWpfAdmin.WelcomeWindowPages
{
    /// <summary>
    /// Логика взаимодействия для PasswordChangePage.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        private readonly INavigationService _navigationService;
        private readonly IAccountService _accountService;
        private readonly ICaptchaService _captchaService;

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
    }
}
