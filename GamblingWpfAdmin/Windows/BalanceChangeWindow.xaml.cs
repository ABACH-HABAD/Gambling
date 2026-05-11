using BusinessLogic.Account.Auth;
using BusinessLogic.Captcha;
using DataBaseClasses.Entity;
using GamblingWpfAdmin.CaptchaAdapters;
using GamblingWpfAdmin.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GamblingWpfAdmin.Windows
{
    /// <summary>
    /// Логика взаимодействия для BalanceChangeWindow.xaml
    /// </summary>
    public partial class BalanceChangeWindow : Window
    {
        private const string DEFAULT_TITLE = "Изменение баланса";

        private readonly INavigationService _navigationService;
        private readonly IAccountService _accountService;
        private readonly ICaptchaService _captchaService;

        internal User? UserContext { get; set; }

        public BalanceChangeWindow(INavigationService navigationService, IAccountService accountService, [FromKeyedServices("simple")] ICaptchaService captchaService)
        {
            _navigationService = navigationService;
            _accountService = accountService;
            _captchaService = captchaService;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Height = 300;
            Width = 800;
            Title = UserContext == null ? DEFAULT_TITLE : $"{(UserContext.Name ?? UserContext.Email)} - {DEFAULT_TITLE}";
        }
    }
}
