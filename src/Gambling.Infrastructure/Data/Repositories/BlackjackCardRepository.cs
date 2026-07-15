using Microsoft.EntityFrameworkCore;
using Gambling.Core.Models;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Infrastructure.Data.Entities;

namespace Gambling.Infrastructure.Data.Repositories;

public class BlackjackCardRepository(ApplicationContext context) : BaseRepository(context), IBlackjackCardRepository
{
    public async Task SaveGameStateAsync(int gameId, List<BlackjackCardModel> cardList)
    {
        foreach (BlackjackCardModel card in cardList)
        {
            card.GameId = gameId;
            await _dataBaseContext.Blackjack_cards.AddAsync(new BlackjackCardEntity()
            {
                Denomination = card.Denomination,
                GameId = gameId,
                InPlayerHand = card.InPlayerHand,
                IsOpen = card.IsOpen,
                Suit = card.Suit
            });
        }
        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task DeleteGameStateAsync(int gameId)
    {
        await _dataBaseContext.Blackjack_cards.Where(card => card.GameId == gameId).ExecuteDeleteAsync();
        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task<bool> HasSaveGameAsync(int gameId)
    {
        return await _dataBaseContext.Blackjack_cards.FirstOrDefaultAsync(card => card.GameId == gameId) is not null;
    }

    public async Task<List<BlackjackCardModel>> GetSaveGameAsync(int gameId)
    {
        return await _dataBaseContext.Blackjack_cards
        .Select(card => new BlackjackCardModel()
        {
            Id = card.Id,
            GameId = card.GameId,
            Denomination = card.Denomination,
            InPlayerHand = card.InPlayerHand,
            IsOpen = card.IsOpen,
            Suit = card.Suit,
            BlackjackGame = card.BlackjackGame != null ? new BlackjackGameModel()
            {
                Id = card.BlackjackGame.Id,
                Bet = card.BlackjackGame.Bet,
                CanPlayerMove = card.BlackjackGame.CanPlayerMove,
                UserId = card.BlackjackGame.UserId,
                User = card.BlackjackGame.User != null ? new UserModel()
                {
                    Id = card.BlackjackGame.User.Id,
                    Email = card.BlackjackGame.User.Email,
                    Name = card.BlackjackGame.User.Name,
                    Coefficient = card.BlackjackGame.User.Coefficient,
                    WinBalance = card.BlackjackGame.User.WinBalance,
                    WinCount = card.BlackjackGame.User.WinCount,
                    LossBalance = card.BlackjackGame.User.LossBalance,
                    LossCount = card.BlackjackGame.User.LossCount,
                    Balance = card.BlackjackGame.User.Balance,
                    IsBlocked = card.BlackjackGame.User.IsBlocked,
                    SlotsBonusCount = card.BlackjackGame.User.SlotsBonusCount,
                    Status = card.BlackjackGame.User.Status != null ? new UserStatusModel()
                    {
                        Id = card.BlackjackGame.User.Status.Id,
                        Name = card.BlackjackGame.User.Status.Name
                    } : null
                } : null
            } : null
        })
        .Where(card => card.GameId == gameId)
        .ToListAsync();
    }
}