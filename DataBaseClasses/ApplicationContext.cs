using DataBaseClasses.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataBaseClasses;

public class ApplicationContext : DbContext
{
    private readonly string dataBaseConnectionString;

    public bool IsConnected { get; private set; } = false;

    internal DbSet<User> Users => Set<User>();
    internal DbSet<Session> Sessions => Set<Session>();
    internal DbSet<Game> Games => Set<Game>();
    internal DbSet<GameType> Game_types => Set<GameType>();
    internal DbSet<DeviceType> Device_types => Set<DeviceType>();
    internal DbSet<UserStatus> User_statuses => Set<UserStatus>();

    public ApplicationContext(DataBaseConnectionString connectionString)
    {
        dataBaseConnectionString = connectionString.ConnectionString;

        IsConnected = Connect();
    }

    public bool Connect()
    {
        if (!Database.CanConnect())
        {
            IsConnected = false;
            return false;
        }

        Database.EnsureCreated();
        IsConnected = false;
        return true;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(dataBaseConnectionString);
    }
}
