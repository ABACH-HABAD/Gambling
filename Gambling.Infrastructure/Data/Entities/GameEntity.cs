using Gambling.Core.Models;

namespace Gambling.Infrastructure.Data.Entities;

internal class GameEntity : BaseEntity<GameEntity, GameModel>
{
    public int PlayerId { get; set; }
    public int GameTypeId { get; set; }
    public double Bet { get; set; }
    public bool IsWin { get; set; }
    public double WinAmount { get; set; }

    public UserEntity? Player { get; set; }
    public GameTypeEntity? GameType { get; set; }

    internal override GameModel ToModel()
    {
        return new()
        {
            Id = Id,
            PlayerId = PlayerId,
            Bet = Bet,
            IsWin = IsWin,
            WinAmount = WinAmount,
            Player = Player?.ToModel(),
            GameType = GameType?.ToModel()
        };
    }
}