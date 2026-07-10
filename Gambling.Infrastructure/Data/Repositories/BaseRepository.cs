using Gambling.Core.Abstractions.Repositories;
using Gambling.Infrastructure.Data;

namespace Gambling.Infrastructure.Data.Repositories;

public abstract class BaseRepository(ApplicationContext context) : IRepository
{
    protected ApplicationContext _dataBaseContext = context;

    public async Task<bool> CheckConncetionAsync()
    {
        bool conncetionStatus = await _dataBaseContext.Database.CanConnectAsync();
        if (conncetionStatus == false) return _dataBaseContext.Connect();
        else return conncetionStatus;
    }

    public bool CheckConncetion()
    {
        bool conncetionStatus = _dataBaseContext.Database.CanConnect();
        if (conncetionStatus == false) return _dataBaseContext.Connect();
        else return conncetionStatus;
    }
}