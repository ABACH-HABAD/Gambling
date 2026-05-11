using BusinessLogic.Account.Auth;
using BusinessLogic.Captcha;
using DataBaseClasses.Entity;
using GamblingWpfAdmin.CaptchaAdapters;
using GamblingWpfAdmin.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GamblingWpfAdmin.Windows
{
    /// <summary>
    /// Логика взаимодействия для RoleChangeWindow.xaml
    /// </summary>
    public partial class RoleChangeWindow : Window
    {
        private const string DEFAULT_TITLE = "Блокировака пользователя";

        private readonly INavigationService _navigationService;
        private readonly IAccountService _accountService;
        private readonly ICaptchaService _captchaService;

        internal User? UserContext { get; set; }

        public RoleChangeWindow(INavigationService navigationService, IAccountService accountService, [FromKeyedServices("simple")] ICaptchaService captchaService)
        {
            _navigationService = navigationService;
            _accountService = accountService;
            _captchaService = captchaService;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Height = 225;
            Width = 475;
            Title = UserContext == null ? DEFAULT_TITLE : $"{(UserContext.Name ?? UserContext.Email)} - {DEFAULT_TITLE}";
        }
    }
}
