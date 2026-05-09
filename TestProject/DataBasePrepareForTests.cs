using DataBaseClasses;
using DataBaseClasses.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject;

public class DataBasePrepareForTests(ApplicationContext _applicationContext, IServiceProvider _serviceProvider)
{
    public async Task PrepareDataBaseAsync()
    {
        //Пересоздаём БД
        await CreateTestDataBaseAsync();

        //Заполняем рыбными данными
        await SeedTestDataAsync();
    }

    public async Task CreateTestDataBaseAsync()
    {
        await _applicationContext.Database.EnsureDeletedAsync();
        await _applicationContext.Database.EnsureCreatedAsync();
    }

    public async Task SeedTestDataAsync()
    {
        if (_applicationContext == null) throw new Exception("Подключение к БД не инициализированно! Используйте InitializeAsync()");

        _serviceProvider = TestServiceProvider.ProvideServer();

        List<ISeedable> seedables = [];

        seedables.Add(_serviceProvider.GetRequiredService<IUserStatusesRepository>());
        seedables.Add(_serviceProvider.GetRequiredService<IGameTypesRepository>());
        seedables.Add(_serviceProvider.GetRequiredService<IDeviceTypeRepository>());

        foreach (ISeedable seedable in seedables) await seedable.SeedDataAsync();
    }
}
