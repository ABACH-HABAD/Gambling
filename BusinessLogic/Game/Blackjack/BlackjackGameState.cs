namespace BusinessLogic.Game.Blackjack;

public record class BlackjackGameState(List<Card> PlayerCards, List<Card> DealerCards);