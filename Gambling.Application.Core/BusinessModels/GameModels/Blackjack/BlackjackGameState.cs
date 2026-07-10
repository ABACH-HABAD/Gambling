namespace Gambling.Application.Core.BusinessModels.GameModels.Blackjack;

public record class BlackjackGameState(bool IsOk, List<Card> PlayerCards, List<Card> DealerCards, double Bet = 0, string? Message = null);