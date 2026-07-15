using Gambling.Core.Models;

namespace Gambling.Infrastructure.Data.Entities;

internal class BlackjackCardEntity : BaseEntity<BlackjackCardEntity, BlackjackCardModel>
{
    public int GameId { get; set; }
    public string Denomination { get; set; } = string.Empty;
    public char Suit { get; set; }
    public bool InPlayerHand { get; set; }
    public bool IsOpen { get; set; }
    public BlackjackGameEntity? BlackjackGame { get; set; }

    internal override BlackjackCardModel ToModel()
    {
        return new BlackjackCardModel()
        {
            Id = Id,
            GameId = GameId,
            Denomination = Denomination,
            Suit = Suit,
            InPlayerHand = InPlayerHand,
            IsOpen = IsOpen,
            BlackjackGame = BlackjackGame?.ToModel()
        };
    }
}