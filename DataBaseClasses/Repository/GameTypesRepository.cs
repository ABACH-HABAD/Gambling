using DataBaseClasses.Repository.Interfaces;

namespace DataBaseClasses.Repository;

public class GameTypesRepository(ApplicationContext dataBaseContext) : BaseRepository(dataBaseContext), IGameTypesRepository
{
    public void SeedData()
    {
        /*
            1	слоты    
            2	блекджек
            3	рулетка
        */

        _dataBaseContext.Game_types.Add(new Entity.GameType() { Id = 1, Name = "слоты" });
        _dataBaseContext.Game_types.Add(new Entity.GameType() { Id = 2, Name = "блекджек" });
        _dataBaseContext.Game_types.Add(new Entity.GameType() { Id = 3, Name = "рулетка" });

        _dataBaseContext.SaveChanges();
    }

    public async Task SeedDataAsync()
    {
        await _dataBaseContext.Game_types.AddAsync(new Entity.GameType() { Id = 1, Name = "слоты" });
        await _dataBaseContext.Game_types.AddAsync(new Entity.GameType() { Id = 2, Name = "блекджек" });
        await _dataBaseContext.Game_types.AddAsync(new Entity.GameType() { Id = 3, Name = "рулетка" });

        await _dataBaseContext.SaveChangesAsync();
    }

    public bool HasThisGameType(int id)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        return _dataBaseContext.Game_types.Find(id) != null;
    }

    public bool HasThisGameType(string name)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        return _dataBaseContext.Game_types.FirstOrDefault(gameType => gameType.Name == name) != null;
    }
}
