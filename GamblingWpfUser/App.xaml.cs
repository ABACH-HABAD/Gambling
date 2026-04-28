using BusinessLogic.ApiServices;
using BusinessLogic.Auth;
using BusinessLogic.Auth.Validation;
using BusinessLogic.Captcha;
using BusinessLogic.Encryption;
using BusinessLogic.Game.Roulette;
using BusinessLogic.Game.Slots;
using BusinessLogic.Token;
using GamblingWpfUser.Navigation;
using GamblingWpfUser.Pages;
using GamblingWpfUser.Pages.Games;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.IO;
using System.Windows;

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

            services.AddTransient<IValidation, EmailValidation>();
            services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();
            services.AddScoped<IAccountService, ClientAccountService>();
            services.AddScoped<ILoginChecker, ClientLoginChecker>();

            services.AddScoped<ISlotsCombinationCounterService, SlotsCombinationCounterService>();
            services.AddScoped<ISlotsWinCounterService, SlotsWinCounterService>();
            services.AddScoped<ISlotsService, ClientSlotsService>();

            services.AddScoped<IRouletteService, ClientRouletteService>();

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