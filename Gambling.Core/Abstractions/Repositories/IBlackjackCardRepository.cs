using Gambling.Core.Models;

namespace Gambling.Core.Abstractions.Repositories;

public interface IBlackjackCardRepository
{
    public Task SaveGameStateAsync(int gameId, List<BlackjackCardModel> cardList);
    public Task DeleteGameStateAsync(int gameId);
    public Task<bool> HasSaveGameAsync(int gameId);
    public Task<List<BlackjackCardModel>> GetSaveGameAsync(int gameId);
}