using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Abstractions.Statistics;
using Gambling.Application.Core.Api.Results;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Models;

namespace Gambling.Application.Server.Services.Account.Profile.Statistics;

public class ServerUserStatisticsService(IStatisticsService statisticsService, IAccountDataService accountDataService, IGameRepository gameRepository) : IUserStatisticsService
{
    public async Task<UserStatisticResult> GetUserStatisticResultAsync(int userId, GameType type)
    {
        UserModel? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetUserStatisticResultAsync(user, type);
    }

    public async Task<double> GetWinFrequencyAsync(int userId, GameType type)
    {
        UserModel? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetWinFrequencyAsync(user, type);
    }

    public async Task<int> GetWinCountAsync(int userId, GameType type)
    {
        UserModel? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetWinCountAsync(user, type);
    }

    public async Task<int> GetLossCountAsync(int userId, GameType type)
    {
        UserModel? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetLossCountAsync(user, type);
    }

    public async Task<int> GetTotalCountAsync(int userId, GameType type)
    {
        UserModel? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetTotalCountAsync(user, type);
    }

    public async Task<double> GetWinBalanceAsync(int userId, GameType type)
    {
        UserModel? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetWinBalanceAsync(user, type);
    }

    public async Task<double> GetLossBalanceAsync(int userId, GameType type)
    {
        UserModel? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetLossBalanceAsync(user, type);
    }

    public async Task<double> GetTotalBalanceAsync(int userId, GameType type)
    {
        UserModel? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetTotalBalanceAsync(user, type);
    }

    public async Task<UserStatisticResult> GetUserStatisticResultAsync(UserModel user, GameType type)
    {
        double WinFrequency = await GetWinFrequencyAsync(user, type);
        int WinCount = await GetWinCountAsync(user, type);
        int LossCount = await GetLossCountAsync(user, type);
        double WinBalance = await GetWinBalanceAsync(user, type);
        double LossBalance = await GetLossBalanceAsync(user, type);
        double TotalBalance = await GetTotalBalanceAsync(user, type);
        int TotalCount = await GetTotalCountAsync(user, type);

        return new UserStatisticResult
            (WinFrequency, WinCount, LossCount, TotalCount, WinBalance, LossBalance, TotalBalance);
    }

    public async Task<double> GetWinFrequencyAsync(UserModel user, GameType type)
    {
        switch (type)
        {
            case GameType.None: return 0;
            case GameType.Any:
                double result = statisticsService.WinFrequency(user.WinCount, user.LossCount);
                return result;
            default:
                int win = (await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: true)).Count;
                int loss = (await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: false)).Count;
                return statisticsService.WinFrequency(win, loss);
        }
    }

    public async Task<int> GetWinCountAsync(UserModel user, GameType type)
    {
        return type switch
        {
            GameType.None => 0,
            GameType.Any => user.WinCount,
            _ => (await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: true)).Count,
        };
    }

    public async Task<int> GetLossCountAsync(UserModel user, GameType type)
    {
        return type switch
        {
            GameType.None => 0,
            GameType.Any => user.LossCount,
            _ => (await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: false)).Count,
        };
    }

    public async Task<int> GetTotalCountAsync(UserModel user, GameType type) => await GetWinCountAsync(user, type) - await GetLossCountAsync(user, type);

    public async Task<double> GetWinBalanceAsync(UserModel user, GameType type)
    {
        switch (type)
        {
            case GameType.None: return 0;
            case GameType.Any: return user.WinBalance;
            default:
                double win = 0;
                List<GameModel> games = await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: true);
                foreach (GameModel game in games) win += game.WinAmount;
                return win;
        }
    }

    public async Task<double> GetLossBalanceAsync(UserModel user, GameType type)
    {
        switch (type)
        {
            case GameType.None: return 0;
            case GameType.Any: return user.LossBalance;
            default:
                double win = 0;
                List<GameModel> games = await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: false);
                foreach (GameModel game in games) win += game.Bet;
                return win;
        }
    }
    public async Task<double> GetTotalBalanceAsync(UserModel user, GameType type) => await GetWinBalanceAsync(user, type) - await GetLossBalanceAsync(user, type);
}
