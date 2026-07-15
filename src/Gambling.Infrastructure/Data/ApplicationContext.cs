using Microsoft.EntityFrameworkCore;
using Gambling.Infrastructure.Data.Entities;

namespace Gambling.Infrastructure.Data;

public class ApplicationContext(DataBaseConnectionString connectionString) : DbContext
{
    private readonly string dataBaseConnectionString = connectionString.ConnectionString;

    public bool IsConnected => Database.CanConnect();

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(dataBaseConnectionString);
        base.OnConfiguring(optionsBuilder);
    }
}