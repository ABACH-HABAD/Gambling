using BusinessLogic.Auth;
using BusinessLogic.Captcha;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GamblingWpfAdmin.WelcomeWindowPages
{
    /// <summary>
    /// Логика взаимодействия для UserBanPage.xaml
    /// </summary>
    public partial class UserBanPage : Page
    {
        private readonly INavigationService _navigationService;
        private readonly IAccountService _accountService;
        private readonly ICaptchaService _captchaService;

        public UserBanPage(INavigationService navigationService, IAccountService accountService, [FromKeyedServices("simple")] ICaptchaService captchaService)
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
