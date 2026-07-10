using Gambling.Infrastructure.Data;
using Gambling.WebApi.Classes;
using Gambling.WebApi.Endpoints;
using Gambling.WebApi.Middleware;

namespace Gambling.WebApi;

public partial class Programm
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = CreateBuilder(args);

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

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
        using (IServiceScope service = app.Services.CreateScope())
        {
            ApplicationContext context = app.Services.GetRequiredService<ApplicationContext>();
            context.Database.EnsureCreated();
            Console.WriteLine("Приложение успешно запущено!");
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

        builder.Services.AddJwtAuthentication(builder.Configuration);
        builder.Services.AddDataBase(builder.Configuration);
        builder.Services.AddDataBaseServices();
        builder.Services.AddBusinessServices();
        builder.Services.AddBackendService();
        builder.Services.AddOpenApi();

        return builder;
    }

    public static IHost CreateHost(string[] args)
    {
        WebApplicationBuilder builder = CreateBuilder(args);
        return builder.Build();
    }
}