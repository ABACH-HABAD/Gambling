using Gambling.Core.Models;

namespace Gambling.Infrastructure.Data.Entities;

internal class BlackjackGameEntity : BaseEntity<BlackjackGameEntity, BlackjackGameModel>
{
    public int UserId { get; set; }
    public double Bet { get; set; }
    public bool CanPlayerMove { get; set; }

    public List<BlackjackCardEntity> Cards { get; set; } = [];
    public UserEntity? User { get; set; }

    internal override BlackjackGameModel ToModel()
    {
        BlackjackGameModel dto = new()
        {
            Id = Id,
            UserId = UserId,
            Bet = Bet,
            CanPlayerMove = CanPlayerMove
        };
        if (User != null) dto.User = User.ToModel();

        List<BlackjackCardModel> cardModels = [];
        foreach (BlackjackCardEntity card in Cards)
        {
            cardModels.Add(card.ToModel());
        }
        dto.Cards = cardModels;

        return dto;
    }
}