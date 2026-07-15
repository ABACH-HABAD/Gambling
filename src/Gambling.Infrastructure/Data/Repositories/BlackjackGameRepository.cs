using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Gambling.Infrastructure.Data.Projections;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Models;
using Gambling.Infrastructure.Data.Entities;

namespace Gambling.Infrastructure.Data.Repositories;

public class BlackjackGameRepository(ApplicationContext context) : BaseRepository(context), IBlackjackGameRepository
{
    public async Task<BlackjackGameModel> SaveGameStateAsync(int userId, BlackjackGameModel gameState)
    {
        await _dataBaseContext.Blackjack_games.AddAsync(new BlackjackGameEntity()
        {
            UserId = gameState.UserId,
            Bet = gameState.Bet,
            CanPlayerMove = gameState.CanPlayerMove
        });
        await _dataBaseContext.SaveChangesAsync();

        return await _dataBaseContext.Blackjack_games
        .ToBlackjackGameModel()
        .Where(game => game.UserId == userId)
        .FirstAsync();
    }

    public async Task DeleteGameStateAsync(int userId)
    {
        await _dataBaseContext.Blackjack_games.Where(game => game.UserId == userId).ExecuteDeleteAsync();
        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task<bool> HasSaveGameAsync(int userId)
    {
        return await _dataBaseContext.Blackjack_games.FirstOrDefaultAsync(game => game.UserId == userId) is not null;
    }

    public async Task<BlackjackGameModel> GetSaveGameAsync(int userId)
    {
        return await _dataBaseContext.Blackjack_games
        .ToBlackjackGameModel()
        .FirstAsync(game => game.UserId == userId);
    }
}