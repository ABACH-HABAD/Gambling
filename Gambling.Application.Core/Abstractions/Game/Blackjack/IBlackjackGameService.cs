using Gambling.Application.Core.BusinessModels.GameModels.Blackjack;

namespace Gambling.Application.Core.Abstractions.Game.Blackjack;

public interface IBlackjackGameService
{
    public bool CanPlayerMove { get; }
    public int PlayerId { get; }
    public double Bet { get; }
    public BlackjackPlayer Dealer { get; }
    public BlackjackPlayer Player { get; }

    public Task StartGameAsync(int playerId, double bet);
    public Task ContinueGameAsync(int playerId);
    public Task EndGameAsync();
    public Task PlayerEndMoveAsync();
    public Task AddToBetAsync(double bet);
    public Task DoubleBetAsync();
    public Task AddNextCardToPlayerAsync(BlackjackPlayer player, bool open = true);

    public BlackjackGameState FormGameState(bool allDealerCardsOpen = false);
}