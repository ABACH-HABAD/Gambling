using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BusinessLogic.ApiServices;
using BusinessLogic.Captcha;
using BusinessLogic.Encryption;
using BusinessLogic.Game.Roulette;
using BusinessLogic.Game.Slots;
using BusinessLogic.Token;
using BusinessLogic.Validation;
using GamblingWpfUser.Navigation;
using GamblingWpfUser.Pages;
using GamblingWpfUser.Pages.Games;
using BusinessLogic.Account.Auth;
using BusinessLogic.Account.Balance;
using BusinessLogic.Account.Profile.Statistics;
using BusinessLogic.Account;
using GamblingWpfUser.Windows;
using BusinessLogic.Game.Blackjack;

namespace GamblingWpfUser;

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

        IHostBuilder builder = Host.CreateDefaultBuilder();

        string basePath = AppContext.BaseDirectory;
        string appsettingsPath = Path.Combine(basePath, "appsettings.json");

        ConfigurationBuilder configurationBuilder = new();
        configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
        configurationBuilder.AddJsonFile(appsettingsPath, optional: false, reloadOnChange: true);

        IConfigurationRoot Configuration = configurationBuilder.Build();

        IConfigurationSection apiSettings = Configuration.GetSection("ApiSettings") ?? throw new Exception("ApiSettings не найден");

        string apiUrl = apiSettings["ApiUrl"] ?? throw new Exception($"{nameof(apiUrl)} не настроен");
        int timeout = int.Parse(apiSettings["TimeoutSeconds"] ?? throw new Exception($"{nameof(timeout)} не настроен"));

        builder.ConfigureServices((context, services) =>
        {
            services.AddSingleton(new ApiSettings(apiUrl, timeout));

            services.AddSingleton<IApiClient, ApiClient>();
            services.AddKeyedSingleton<ITokenStorageService, AccessTokenStorageService>("access");
            services.AddKeyedSingleton<ITokenStorageService, RefreshTokenStorageService>("refresh");

            services.AddTransient<IPasswordHasher, Encryption>();
            services.AddTransient<IEncryption, Encryption>();

            services.AddScoped<ICardPayService, ClientBalanceService>();

            services.AddTransient<INameValidation, NameValidationService>();
            services.AddTransient<IEmailValidation, EmailValidation>();
            services.AddTransient<ICardValidation, CardValidationService>();
            services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();

            services.AddScoped<ILoginChecker, ClientLoginChecker>();
            services.AddScoped<IAccountService, ClientAccountService>();
            services.AddScoped<IAccountDataService, ClientAccountDataService>();

            services.AddScoped<ISlotsCombinationCounterService, SlotsCombinationCounterService>();
            services.AddScoped<ISlotsWinCounterService, SlotsWinCounterService>();
            services.AddScoped<ISlotsService, ClientSlotsService>();

            services.AddScoped<IRouletteService, ClientRouletteService>();

            services.AddScoped<IBlackjackService, ClientBlackjackService>();

            services.AddTransient<IUserStatisticsService, ClientUserStatisticService>();

            services.AddKeyedScoped<ICaptchaService, EasyCaptchaService>("easy");
            services.AddKeyedScoped<ICaptchaService, SimpleCaptchaService>("simple");

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<MainWindow>();

            services.AddSingleton<AuthPage>();
            services.AddSingleton<RegistrationPage>();
            services.AddSingleton<GamesPage>();
            services.AddSingleton<SlotsPage>();
            services.AddSingleton<RoulettePage>();
            services.AddSingleton<BlackjackPage>();
            services.AddSingleton<ProfilePage>();

            services.AddTransient<PayWindow>();
            services.AddTransient<EmailChangeWindow>();
            services.AddTransient<PasswordChangeWindow>();

        });

        _host = builder.Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();
        Services = _host.Services;

        IApiClient apiClient = Services.GetRequiredService<IApiClient>();
        await apiClient.LoadTokenAsync();

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