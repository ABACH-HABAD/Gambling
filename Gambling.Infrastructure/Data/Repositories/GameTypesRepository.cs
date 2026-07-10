using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Exceptions;
using Gambling.Infrastructure.Data;
using Gambling.Infrastructure.Data.Entities;

namespace Gambling.Infrastructure.Data.Repositories;

public class GameTypesRepository(ApplicationContext dataBaseContext) : BaseRepository(dataBaseContext), IGameTypesRepository
{
    public void SeedData()
    {
        /*
            1	слоты    
            2	блекджек
            3	рулетка
        */

        _dataBaseContext.Game_types.Add(new GameTypeEntity() { Id = 1, Name = "слоты" });
        _dataBaseContext.Game_types.Add(new GameTypeEntity() { Id = 2, Name = "блекджек" });
        _dataBaseContext.Game_types.Add(new GameTypeEntity() { Id = 3, Name = "рулетка" });

        _dataBaseContext.SaveChanges();
    }

    public async Task SeedDataAsync()
    {
        await _dataBaseContext.Game_types.AddAsync(new GameTypeEntity() { Id = 1, Name = "слоты" });
        await _dataBaseContext.Game_types.AddAsync(new GameTypeEntity() { Id = 2, Name = "блекджек" });
        await _dataBaseContext.Game_types.AddAsync(new GameTypeEntity() { Id = 3, Name = "рулетка" });

        await _dataBaseContext.SaveChangesAsync();
    }

    public bool HasThisGameType(int id)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        GameTypeEntity? game = _dataBaseContext.Game_types.FirstOrDefault(gameType => gameType.Id == id);
        bool hasThisGameType = game != null;
        return hasThisGameType;
    }

    public bool HasThisGameType(string name)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        return _dataBaseContext.Game_types.FirstOrDefault(gameType => gameType.Name == name) != null;
    }
}