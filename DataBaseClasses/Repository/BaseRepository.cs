using DataBaseClasses.Repository.Interfaces;

namespace DataBaseClasses.Repository;

public abstract class BaseRepository(ApplicationContext context) : IRepository
{
    protected ApplicationContext _dataBaseContext = context;

    public bool CheckConncetion()
    {
        bool conncetionStatus = _dataBaseContext.Database.CanConnect();
        if (conncetionStatus == false) return _dataBaseContext.Connect();
        else return conncetionStatus;
    }
}
