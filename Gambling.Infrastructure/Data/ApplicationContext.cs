using Microsoft.EntityFrameworkCore;
using Gambling.Infrastructure.Data.Entities;

namespace Gambling.Infrastructure.Data;

public class ApplicationContext : DbContext
{
    private readonly string dataBaseConnectionString;

    public bool IsConnected { get; private set; } = false;

    internal DbSet<UserEntity> Users => Set<UserEntity>();
    internal DbSet<SessionEntity> Sessions => Set<SessionEntity>();
    internal DbSet<GameEntity> Games => Set<GameEntity>();
    internal DbSet<GameTypeEntity> Game_types => Set<GameTypeEntity>();
    internal DbSet<DeviceTypeEntity> Device_types => Set<DeviceTypeEntity>();
    internal DbSet<UserStatusEntity> User_statuses => Set<UserStatusEntity>();
    internal DbSet<PromotionalCodeEntity> Promotional_codes => Set<PromotionalCodeEntity>();
    internal DbSet<PromotionalCodesActivateEntity> Promotional_codes_activates => Set<PromotionalCodesActivateEntity>();
    internal DbSet<BlackjackCardEntity> Blackjack_cards => Set<BlackjackCardEntity>();
    internal DbSet<BlackjackGameEntity> Blackjack_games => Set<BlackjackGameEntity>();

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
