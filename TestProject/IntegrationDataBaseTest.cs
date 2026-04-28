using BusinessLogic;
using DataBaseClasses;
using DataBaseClasses.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject;

public abstract class IntegrationDataBaseTest : DependencyOnServicesTest
{
    protected ApplicationContext _applicationContext = null!;

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _applicationContext = _serviceProvider.GetService<ApplicationContext>() ?? throw new NoConnectionException();

        //Пересоздаём БД
        await _applicationContext.Database.EnsureDeletedAsync();
        await _applicationContext.Database.EnsureCreatedAsync();

        //Заполняем рыбными данными
        await SeedTestDataAsync();
    }

    protected async Task SeedTestDataAsync()
    {
        if (_applicationContext == null) throw new Exception("Подключение к БД не инициализированно! Используйте InitializeAsync()");

        _serviceProvider = TestServiceProvider.Provide();

        List<ISeedable> seedables = [];

        seedables.Add(_serviceProvider.GetRequiredService<IUserStatusesRepository>());
        seedables.Add(_serviceProvider.GetRequiredService<IGameTypesRepository>());
        seedables.Add(_serviceProvider.GetRequiredService<IDeviceTypeRepository>());

        foreach (ISeedable seedable in seedables) await seedable.SeedDataAsync();
    }
}
