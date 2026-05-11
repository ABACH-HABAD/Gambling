using BusinessLogic.Account.Auth;
using BusinessLogic.Captcha;
using GamblingWpfAdmin.CaptchaAdapters;
using GamblingWpfAdmin.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace GamblingWpfAdmin.WelcomeWindowPages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private readonly INavigationService _navigationService;
        private readonly IAccountService _accountService;
        private readonly ICaptchaService _captchaService;

        public RegistrationPage(INavigationService navigationService, IAccountService accountService, [FromKeyedServices("easy")] ICaptchaService captchaService)
        {
            _navigationService = navigationService;
            _accountService = accountService;
            _captchaService = captchaService;

            InitializeComponent();

            ICaptchaView captchaView = new EasyCaptchaAdapter(CaptchaPlace);

            _captchaService.InitializationChaptcha(captchaView, 3);
            _captchaService.GenerateChaptcha(5, CaptchaPattern.NumbersAndLetters);
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<AuthPage>();
        }

        private async void Registration_Click(object sender, RoutedEventArgs e)
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

            LoginResult loginResult = await _accountService.RegistrateAsync(Login.Text, Password.Password, RepeatPassword.Password, DeviceType.Windows);
            if (loginResult.Result)
            {
                Login.Text = string.Empty;
                Password.Password = string.Empty;
                RepeatPassword.Password = string.Empty;

                MessageBox.Show("Данная версия ИС \"Азартные игры\" предназначена только для сотрудников организации.\n\nЕсли вы являетесь сотрудником и не можете войти, обратитесь к вашему начальнику.");
            }
            else MessageBox.Show(loginResult.Message);
        }
    }
}
