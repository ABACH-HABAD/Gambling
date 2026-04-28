using BusinessLogic.Game.Slots;
using DataBaseClasses;
using DataBaseClasses.Entity;
using DataBaseClasses.Repository;
using DataBaseClasses.Repository.Interfaces;
using System.Security.Cryptography;

namespace BusinessLogic.Game.Roulette;

public class ServerRouletterService(IUserRepository userRepository, IGameRepository gameRepository, IRouletteWinCounterService rouletteWinCounterService) : IRouletteService, IGameService
{
    private const int GAMETYPE = 3;

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

            userRepository.WriteOffFromBalance(userId, totalBid);
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

        userRepository.AddToBalance(userId, winCount);

        gameRepository.AddGame(userId, GAMETYPE, totalBid, winCount > 0, winCount);

        return new RouletteGameResult(true, $"Вы выиграли {winCount}", winCount, winElement.Value);
    }
}
