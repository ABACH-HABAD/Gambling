using Microsoft.EntityFrameworkCore;
using Gambling.Infrastructure.Data.Projections;
using Gambling.Infrastructure.Data.Entities;
using Gambling.Core.Models;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Exceptions;

namespace Gambling.Infrastructure.Data.Repositories;

public class GameRepository(ApplicationContext context) : BaseRepository(context), IGameRepository
{
    protected async Task<GameModel?> GetWithId(int id)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        GameModel? game = await _dataBaseContext.Games
       .ToGameModel()
       .FirstOrDefaultAsync(game => game.Id == id);

        return game;
    }

    public async Task AddGameAsync(int playerId, int gameType, double bet, bool isWin, double winAmount)
    {
        GameEntity game = new()
        {
            PlayerId = playerId,
            GameTypeId = gameType,
            Bet = bet,
            IsWin = isWin,
            WinAmount = winAmount
        };

        await _dataBaseContext.Games.AddAsync(game);

        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task<List<GameModel>> GetGamesAsync() => await _dataBaseContext.Games
        .ToGameModel()
        .ToListAsync();

    public async Task<List<GameModel>> GetGamesAsync(int gameType) => await _dataBaseContext.Games
        .ToGameModel()
        .Where(game => game.GameType!.Id == gameType)
        .ToListAsync();

    public async Task<List<GameModel>> GetGamesAsync(int playerId, int gameType) => await _dataBaseContext.Games
        .ToGameModel()
        .Where(game => game.PlayerId == playerId && game.GameType!.Id == gameType)
        .ToListAsync();

    public async Task<List<GameModel>> GetGamesAsync(int playerId, int gameType, bool isWin) => await _dataBaseContext.Games
        .ToGameModel()
        .Where(game => game.PlayerId == playerId && game.GameType!.Id == gameType && game.IsWin == isWin)
        .ToListAsync();
}