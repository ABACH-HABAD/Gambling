using GamblingWebApi.Endpoints;

namespace GamblingWebApi;

public partial class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = CreateBuilder(args);

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapUserEndpoints();
        app.MapUserDataEndpoints();
        app.MapUserStatisticEndpoints();
        app.MapPayEndpoints();
        app.MapGameDataEndpoints();
        app.MapSlotsEndpoints();
        app.MapBlackjackEndpoints();
        app.MapRouletteEndpoints();

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
        builder.Services.AddOpenApi();

        return builder;
    }

    public static IHost CreateHost(string[] args)
    {
        WebApplicationBuilder builder = CreateBuilder(args);
        return builder.Build();
    }
}