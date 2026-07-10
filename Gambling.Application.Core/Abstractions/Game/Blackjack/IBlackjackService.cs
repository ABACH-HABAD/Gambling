using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.BusinessModels.GameModels.Blackjack;

namespace Gambling.Application.Core.Abstractions.Game.Blackjack;

public interface IBlackjackService : IGameService
{
    public Task<BlackjackGameState> TryContinueGameAsync(int userId);
    public Task<BlackjackGameState> FirstMoveAsync(int userId, double bet);
    public Task<BlackjackGameState> TakeCardAsync(int userId);
    public Task<BlackjackGameState> TakeDoubleAsync(int userId);
    public Task<BlackjackGameState> StandAsync(int userId);
    public Task<BlackjackGameResult> EndGameAsync(int userId);
}