using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Gambling.WebApi;
using Gambling.WebApi.Classes;
using Gambling.Infrastructure.Data;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Client.Services.ApiServices;
using Gambling.Core.Exceptions;
using Gambling.Application.Core.Abstractions.Token;

namespace Gambling.Test;

public class WebApplicationTests : DependencyOnServicesTest
{
    protected WebApplicationFactory<Programm> _factory = null!;

    protected ApplicationContext _applicationContext = null!;
    protected DataBasePrepareForTests _dataBasePrepareForTests = null!;

    protected override async Task InitializeAsync(Type type = Type.Client)
    {
        //Загрузка сервера
        await base.InitializeAsync(Type.Server);

        //Настройка базы данных и заполнение её рыбными данными
        _applicationContext = _serviceProvider.GetRequiredService<ApplicationContext>() ?? throw new NoConnectionException();

        _dataBasePrepareForTests = new(_applicationContext, _serviceProvider);
        await ClearDataBaseAsync();

        string dataBaseConnectionString = _serviceProvider.GetRequiredService<DataBaseConnectionString>().ConnectionString;

        //Настройка web сервера
        _factory = new WebApplicationFactory<Programm>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseTestMode();
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(ApplicationContext));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton(new DataBaseConnectionString(dataBaseConnectionString));
                    services.AddDbContext<ApplicationContext>(options => options.UseMySQL(dataBaseConnectionString));
                });


            });

        //Настройка http клиента
        await base.InitializeAsync(Type.Client);

        HttpClient httpClient = _factory.CreateClient();
        string baseAddress = httpClient.BaseAddress?.ToString() ?? "http://localhost";
        ApiSettings apiSettings = new(baseAddress, 30);

        if (_serviceProvider.GetRequiredService<IApiClient>() is TestApiCilent testApi)
        {
            await testApi.InitializeAsync(
                httpClient,
                apiSettings,
                _serviceProvider.GetRequiredKeyedService<ITokenStorageService>("access"),
                _serviceProvider.GetRequiredKeyedService<ITokenStorageService>("refresh"));
        }
        else throw new Exception("Используйте тестовую версию ApiCient");
    }

    protected async Task ClearDataBaseAsync()
    {
        await _dataBasePrepareForTests.CreateTestDataBaseAsync();
        await _dataBasePrepareForTests.SeedTestDataAsync();
    }
}
