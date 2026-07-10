using Gambling.Core.Exceptions;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Application.Core.Abstractions.Game;
using Gambling.Application.Core.Abstractions.Game.Blackjack;
using Gambling.Application.Core;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.BusinessModels.GameModels.Blackjack;

namespace Gambling.Application.Server.Services.Game.Blackjack;

public class ServerBlackjackService(
    IUserRepository userRepository,
    IGameRepository gameRepository,
    IBlackjackGameService blackjackGameService,
    IBlackjackScoresService blackjackScoresService)
    : ServerGameService(userRepository, gameRepository, GameType.Blackjack), IBlackjackService, IGameService
{

    private async Task StartGameAsync(int userId, double bet) => await blackjackGameService.StartGameAsync(userId, bet);
    private async Task ContinueGameAsync(int userId) => await blackjackGameService.ContinueGameAsync(userId);

    private async Task AddCardAsync(BlackjackPlayer player, bool open = true) => await blackjackGameService.AddNextCardToPlayerAsync(player, open);
    private async Task<bool> TryAddCardAsync(BlackjackPlayer player, bool open = true)
    {
        try
        {
            await AddCardAsync(player, open);
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    public async Task<BlackjackGameState> TryContinueGameAsync(int userId)
    {
        try
        {
            await ContinueGameAsync(userId);

            if (blackjackGameService.PlayerId == userId)
            {
                return blackjackGameService.FormGameState();
            }
            else return new BlackjackGameState(false, null!, null!, Message: "Невозможно продолжить игру");
        }
        catch
        {
            return new BlackjackGameState(false, null!, null!, Message: "Невозможно продолжить игру");
        }
    }

    public async Task<BlackjackGameState> FirstMoveAsync(int userId, double bet)
    {
        if (bet <= 0) return new BlackjackGameState(false, null!, null!, Message: "Ставка должна быть больше 0");

        try
        {
            await _userRepository.WriteOffFromBalanceAsync(userId, bet);
        }
        catch (InsufficientFundsException)
        {
            return new BlackjackGameState(false, null!, null!, Message: "Недостаточно средств!");
        }

        await StartGameAsync(userId, bet);

        return blackjackGameService.FormGameState();
    }

    public async Task<BlackjackGameState> TakeCardAsync(int userId)
    {
        await ContinueGameAsync(userId);

        if (!blackjackGameService.CanPlayerMove) return new BlackjackGameState(false, null!, null!, Message: "Сейчас нельзя сделать ход");

        await TryAddCardAsync(blackjackGameService.Player);

        return blackjackGameService.FormGameState();
    }

    public async Task<BlackjackGameState> TakeDoubleAsync(int userId)
    {
        await ContinueGameAsync(userId);

        if (!blackjackGameService.CanPlayerMove) return new BlackjackGameState(false, null!, null!, Message: "Сейчас нельзя сделать ход");

        try
        {
            if (await TryAddCardAsync(blackjackGameService.Player))
            {
                await blackjackGameService.DoubleBetAsync();
                await _userRepository.WriteOffFromBalanceAsync(userId, blackjackGameService.Bet / 2);
            }
        }
        catch (InsufficientFundsException)
        {
            return new BlackjackGameState(false, null!, null!, Message: "Недостаточно средств!");
        }

        return blackjackGameService.FormGameState();
    }

    public async Task<BlackjackGameState> StandAsync(int userId)
    {
        await ContinueGameAsync(userId);

        await blackjackGameService.PlayerEndMoveAsync();

        await DealerPlaying(userId);

        BlackjackGameState result = blackjackGameService.FormGameState(allDealerCardsOpen: true);
        return result;
    }

    public async Task<BlackjackGameResult> EndGameAsync(int userId)
    {
        await ContinueGameAsync(userId);

        BlackjackGameResult result = await GameResultAsync();
        await _userRepository.AddToBalanceAsync(userId, result.Win);

        try
        {
            await _gameRepository.AddGameAsync(userId, (int)GameType.Blackjack, blackjackGameService.Bet, result.Win > 0, result.Win);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        await blackjackGameService.EndGameAsync();

        return result;
    }

    private async Task<BlackjackGameResult> GameResultAsync()
    {
        IBlackjackGameService game = blackjackGameService;

        int playerScores = blackjackScoresService.Scores(game.Player);
        int dealerScores = blackjackScoresService.Scores(game.Dealer);

        if (playerScores > 21) return new BlackjackGameResult(false, "Перебор", 0);
        else if (playerScores == 21 && dealerScores != 21) return new BlackjackGameResult(false, "Blackjack", game.Bet * 2.5);
        else if (playerScores < 21 && playerScores > dealerScores) return new BlackjackGameResult(false, "Победа", game.Bet * 2);
        else if (playerScores < 21 && dealerScores > 21) return new BlackjackGameResult(false, "Победа", game.Bet * 2);
        else if (playerScores == 21 && dealerScores == 21) return new BlackjackGameResult(false, "Ничья", game.Bet);
        else if (playerScores < 21 && playerScores == dealerScores) return new BlackjackGameResult(false, "Ничья", game.Bet);
        else return new BlackjackGameResult(false, "Проигрышь", 0);
    }

    public async Task DealerPlaying(int userId)
    {
        await ContinueGameAsync(userId);

        foreach (Card card in blackjackGameService.Dealer.Cards) card.Open();

        while (blackjackScoresService.Scores(blackjackGameService.Dealer) < 17)
        {
            await Task.Delay(100);
            if (!await TryAddCardAsync(blackjackGameService.Dealer, open: true)) break;
        }
    }
}