using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GamblingWpfAdmin.WelcomeWindowPages;
using GamblingWpfAdmin.Navigation;
using BusinessLogic.Auth;
using BusinessLogic.Captcha;
using BusinessLogic.Auth.Validation;

namespace GamblingWpfAdmin;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    private static IServiceProvider services = null!;
    public static IServiceProvider Services { get => services; private set => services = value; }

    public App()
    {
        Services = null!;

        _host = Host.CreateDefaultBuilder().ConfigureAppConfiguration((context, config) =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory());

            string basePath = AppContext.BaseDirectory;
            string appsettingsPath = Path.Combine(basePath, "appsettings.json");
            config.AddJsonFile(appsettingsPath, optional: false, reloadOnChange: true);

        }).ConfigureServices((context, services) =>
        {
            services.AddTransient<IValidation, EmailValidation>();
            services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();
            services.AddScoped<IAccountService, ClientAccountService>();
            services.AddScoped<ILoginChecker, ClientLoginChecker>();

            services.AddKeyedScoped<ICaptchaService, EasyCaptchaService>("easy");
            services.AddKeyedScoped<ICaptchaService, SimpleCaptchaService>("simple");

            services.AddSingleton<MainWindow>();
            services.AddSingleton<AuthPage>();
            services.AddSingleton<UserBanPage>();
            services.AddSingleton<PasswordChangePage>();
            services.AddSingleton<RegistrationPage>();
            services.AddSingleton<INavigationService, NavigationService>();

        }).Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();
        Services = _host.Services;

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync();
        }

        base.OnExit(e);
    }
}

