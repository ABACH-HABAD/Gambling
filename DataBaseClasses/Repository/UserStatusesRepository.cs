using DataBaseClasses.Repository.Interfaces;

namespace DataBaseClasses.Repository;

public class UserStatusesRepository(ApplicationContext dataBaseContext) : BaseRepository(dataBaseContext), IUserStatusesRepository
{
    public void SeedData()
    {
        /*
            3	Админ
            2	Спонсор
            1	Игрок
        */

        _dataBaseContext.User_statuses.Add(new Entity.UserStatus() { Id = 1, Name = "Игрок" });
        _dataBaseContext.User_statuses.Add(new Entity.UserStatus() { Id = 2, Name = "Спонсор" });
        _dataBaseContext.User_statuses.Add(new Entity.UserStatus() { Id = 3, Name = "Админ" });

        _dataBaseContext.SaveChanges();
    }

    public async Task SeedDataAsync()
    {
        await _dataBaseContext.User_statuses.AddAsync(new Entity.UserStatus() { Id = 1, Name = "Игрок" });
        await _dataBaseContext.User_statuses.AddAsync(new Entity.UserStatus() { Id = 2, Name = "Спонсор" });
        await _dataBaseContext.User_statuses.AddAsync(new Entity.UserStatus() { Id = 3, Name = "Админ" });

        await _dataBaseContext.SaveChangesAsync();
    }
}
