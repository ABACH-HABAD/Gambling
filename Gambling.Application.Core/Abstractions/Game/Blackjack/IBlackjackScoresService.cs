using Gambling.Application.Core.BusinessModels.GameModels.Blackjack;

namespace Gambling.Application.Core.Abstractions.Game.Blackjack;

public interface IBlackjackScoresService
{
    public int Scores(BlackjackPlayer player, bool onlyOpenCards = false);
    public int Scores(List<Card> cards, bool onlyOpenCards = false);
}