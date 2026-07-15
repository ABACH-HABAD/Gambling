using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Gambling.Application.Client.Services.ApiServices;
using Gambling.Wpf.Admin.Classes;

namespace Gambling.Wpf.Admin;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
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
            services.AddBuisnessLogic();
            services.AddWindowsAndPages();
        });

        _host = builder.Build();
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