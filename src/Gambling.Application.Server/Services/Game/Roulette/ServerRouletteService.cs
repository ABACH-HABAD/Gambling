using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Game;
using Gambling.Application.Core.Abstractions.Game.Roulette;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.BusinessModels.GameModels.Roulette;
using Gambling.Application.Server.Services.Game;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Exceptions;

namespace Gambling.Application.Server.Services.Game.Roulette;

public class ServerRouletteService(
    IUserRepository userRepository,
    IGameRepository gameRepository,
    IRouletteWinCounterService rouletteWinCounterService) : ServerGameService(userRepository, gameRepository, GameType.Roulette), IRouletteService, IGameService
{
    private static readonly Random random = new();

    public async Task<RouletteGameResult?> Spin(int userId, List<RouletteBid> bids)
    {
        //Пытаемся списать ставку
        double totalBid = 0;
        try
        {
            foreach (RouletteBid bid in bids)
            {
                totalBid += bid.BidCount;
            }

            await _userRepository.WriteOffFromBalanceAsync(userId, totalBid);
        }
        catch (InsufficientFundsException)
        {
            return new RouletteGameResult(false, "На балансе недостаточно средств", 0, -1);
        }
        catch (AccountNotFoundException)
        {
            return new RouletteGameResult(false, $"Аккаунт не найден #{userId}", 0, -1);
        }

        RouletteElement winElement = new(random.Next(0, 36));

        double winCount = rouletteWinCounterService.WinCount(winElement, bids);

        await _userRepository.AddToBalanceAsync(userId, winCount);

        await _gameRepository.AddGameAsync(userId, (int)DefaultGameType, totalBid, winCount > 0, winCount);

        return new RouletteGameResult(true, $"Вы выиграли {winCount}", winCount, winElement.Value);
    }
}