using Gambling.Core.Abstractions.Repositories;
using Gambling.Infrastructure.Data.Entities;

namespace Gambling.Infrastructure.Data.Repositories;

public class DeviceTypesRepository(ApplicationContext dataBaseContext) : BaseRepository(dataBaseContext), IDeviceTypeRepository
{
    public void SeedData()
    {
        /*
            1	Windows
            2	Приложение администратора
            3	Android
            4	Ios
            5	Браузер
        */

        _dataBaseContext.Device_types.Add(new DeviceTypeEntity() { Id = 1, Name = "Windows" });
        _dataBaseContext.Device_types.Add(new DeviceTypeEntity() { Id = 2, Name = "Приложение администратора" });
        _dataBaseContext.Device_types.Add(new DeviceTypeEntity() { Id = 3, Name = "Android" });
        _dataBaseContext.Device_types.Add(new DeviceTypeEntity() { Id = 4, Name = "Ios" });
        _dataBaseContext.Device_types.Add(new DeviceTypeEntity() { Id = 5, Name = "Браузер" });

        _dataBaseContext.SaveChanges();
    }

    public async Task SeedDataAsync()
    {
        await _dataBaseContext.Device_types.AddAsync(new DeviceTypeEntity() { Id = 1, Name = "Windows" });
        await _dataBaseContext.Device_types.AddAsync(new DeviceTypeEntity() { Id = 2, Name = "Приложение администратора" });
        await _dataBaseContext.Device_types.AddAsync(new DeviceTypeEntity() { Id = 3, Name = "Android" });
        await _dataBaseContext.Device_types.AddAsync(new DeviceTypeEntity() { Id = 4, Name = "Ios" });
        await _dataBaseContext.Device_types.AddAsync(new DeviceTypeEntity() { Id = 5, Name = "Браузер" });

        await _dataBaseContext.SaveChangesAsync();
    }
}