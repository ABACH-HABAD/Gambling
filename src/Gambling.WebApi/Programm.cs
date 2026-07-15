using Gambling.Infrastructure.Data;
using Gambling.WebApi.Classes;
using Gambling.WebApi.Classes.Services;
using Gambling.WebApi.Endpoints;
using Gambling.WebApi.Middleware;
using Microsoft.AspNetCore.DataProtection;
using System.Collections;

namespace Gambling.WebApi;

public partial class Programm
{
    public async static Task Main(string[] args)
    {
        WebApplicationBuilder builder = CreateBuilder(args);

        WebApplication app = builder.Build();

        app.UseCors("AllowAll");
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpLogging();
        app.UseMiddleware();

        //mapping
        app.MapUserEndpoints();
        app.MapUserDataEndpoints();
        app.MapUserStatisticEndpoints();

        app.MapSessionsEndpoints();

        app.MapPayEndpoints();
        app.MapPromotionalCodes();

        app.MapGameDataEndpoints();
        app.MapSlotsEndpoints();
        app.MapBlackjackEndpoints();
        app.MapRouletteEndpoints();

        //подготовка к запуске
        using (IServiceScope scope = app.Services.CreateScope())
        {
            Console.WriteLine($"Running in {builder.Environment.EnvironmentName} environment");

            ApplicationContext context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            int tryesCount = 0;
            int maxTryesCount = 10;
            while (tryesCount < maxTryesCount)
            {
                try
                {
                    context.Database.EnsureCreated();
                    Console.WriteLine("Приложение успешно запущено!");
                    break;
                }
                catch
                {
                    tryesCount++;
                    Console.WriteLine($"Попытка подключения к базе данных {tryesCount} из {maxTryesCount} - неудачно");
                    Thread.Sleep(5000);
                }
            }
        }

        //запуск
        app.Run();
    }

    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

        string basePath = AppContext.BaseDirectory;
        string appsettingsPath = Path.Combine(basePath, "appsettings.json");
        builder.Configuration.AddJsonFile(appsettingsPath, optional: false, reloadOnChange: true);

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(8080); 
            options.ListenLocalhost(5000); 
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
        });

        builder.Services.AddJwtAuthentication(builder.Configuration);
        builder.Services.AddDataBase(builder.Configuration);
        builder.Services.AddDataBaseServices();
        builder.Services.AddBusinessServices();
        builder.Services.AddBackendService();
        builder.Services.AddOpenApi();
        builder.Services.AddHttpLogging();
        builder.Services.AddLogging();

        return builder;
    }

    public static IHost CreateHost(string[] args)
    {
        WebApplicationBuilder builder = CreateBuilder(args);
        return builder.Build();
    }
}