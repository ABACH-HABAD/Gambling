using DataBaseClasses.Exceptions;
using DataBaseClasses.Repository.Interfaces;

namespace BusinessLogic.Game.Roulette;

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

            _userRepository.WriteOffFromBalance(userId, totalBid);
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

        _userRepository.AddToBalance(userId, winCount);

        _gameRepository.AddGame(userId, (int)DefaultGameType, totalBid, winCount > 0, winCount);

        return new RouletteGameResult(true, $"Вы выиграли {winCount}", winCount, winElement.Value);
    }
}
