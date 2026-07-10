using Gambling.Core.Abstractions.Repositories;
using Gambling.Infrastructure.Data.Entities;

namespace Gambling.Infrastructure.Data.Repositories;

public class UserStatusesRepository(ApplicationContext dataBaseContext) : BaseRepository(dataBaseContext), IUserStatusesRepository
{
    public void SeedData()
    {
        /*
            3	Админ
            2	Спонсор
            1	Игрок
        */

        _dataBaseContext.User_statuses.Add(new UserStatusEntity() { Id = 1, Name = "Игрок" });
        _dataBaseContext.User_statuses.Add(new UserStatusEntity() { Id = 2, Name = "Спонсор" });
        _dataBaseContext.User_statuses.Add(new  () { Id = 3, Name = "Админ" });
        _dataBaseContext.User_statuses.Add(new UserStatusEntity() { Id = 4, Name = "Заблокирован" });

        _dataBaseContext.SaveChanges();
    }

    public async Task SeedDataAsync()
    {
        await _dataBaseContext.User_statuses.AddAsync(new UserStatusEntity() { Id = 1, Name = "Игрок" });
        await _dataBaseContext.User_statuses.AddAsync(new UserStatusEntity() { Id = 2, Name = "Спонсор" });
        await _dataBaseContext.User_statuses.AddAsync(new UserStatusEntity() { Id = 3, Name = "Админ" });
        await _dataBaseContext.User_statuses.AddAsync(new UserStatusEntity() { Id = 4, Name = "Заблокирован" });

        await _dataBaseContext.SaveChangesAsync();
    }
}