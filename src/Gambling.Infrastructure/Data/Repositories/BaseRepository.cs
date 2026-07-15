using Gambling.Core.Abstractions.Repositories;
using Gambling.Infrastructure.Data;

namespace Gambling.Infrastructure.Data.Repositories;

public abstract class BaseRepository(ApplicationContext context) : IRepository
{
    protected ApplicationContext _dataBaseContext = context;

    public async Task<bool> CheckConncetionAsync()
    {
        return _dataBaseContext.IsConnected;
    }

    public bool CheckConncetion()
    {
        return _dataBaseContext.IsConnected;
    }
}