using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using DataBaseClasses.Entity;
using BusinessLogic.Captcha;
using GamblingWpfAdmin.CaptchaAdapters;
using GamblingWpfAdmin.Navigation;
using BusinessLogic.Account.Auth;

namespace GamblingWpfAdmin.WelcomeWindowPages
{
    /// <summary>
    /// Логика взаимодействия для UserBanPage.xaml
    /// </summary>
    public partial class UserBanWindow : Window
    {
        private const string DEFAULT_TITLE = "Блокировака пользователя";

        private readonly INavigationService _navigationService;
        private readonly IAccountService _accountService;
        private readonly ICaptchaService _captchaService;

        internal User? UserContext { get; set; }

        public UserBanWindow(INavigationService navigationService, IAccountService accountService, [FromKeyedServices("simple")] ICaptchaService captchaService)
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
            Width = 650;
            Title = UserContext == null ? DEFAULT_TITLE : $"{(UserContext.Name ?? UserContext.Email)} - {DEFAULT_TITLE}";
        }
    }
}
