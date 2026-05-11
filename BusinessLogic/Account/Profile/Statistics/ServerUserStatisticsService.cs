using DataBaseClasses.Repository.Interfaces;
using DataBaseClasses.Entity;

namespace BusinessLogic.Account.Profile.Statistics;

public class ServerUserStatisticsService(IStatisticsService statisticsService, IAccountDataService accountDataService, IGameRepository gameRepository) : IUserStatisticsService
{
    public async Task<UserStatisticResult> GetUserStatisticResultAsync(int userId, Game.GameType type)
    {
        User? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetUserStatisticResultAsync(user, type);
    }

    public async Task<double> GetWinFrequencyAsync(int userId, Game.GameType type)
    {
        User? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetWinFrequencyAsync(user, type);
    }

    public async Task<int> GetWinCountAsync(int userId, Game.GameType type)
    {
        User? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetWinCountAsync(user, type);
    }

    public async Task<int> GetLossCountAsync(int userId, Game.GameType type)
    {
        User? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetLossCountAsync(user, type);
    }

    public async Task<int> GetTotalCountAsync(int userId, Game.GameType type)
    {
        User? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetTotalCountAsync(user, type);
    }

    public async Task<double> GetWinBalanceAsync(int userId, Game.GameType type)
    {
        User? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetWinBalanceAsync(user, type);
    }

    public async Task<double> GetLossBalanceAsync(int userId, Game.GameType type)
    {
        User? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetLossBalanceAsync(user, type);
    }

    public async Task<double> GetTotalBalanceAsync(int userId, Game.GameType type)
    {
        User? user = await accountDataService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await GetTotalBalanceAsync(user, type);
    }

    public async Task<UserStatisticResult> GetUserStatisticResultAsync(User user, Game.GameType type)
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

    public async Task<double> GetWinFrequencyAsync(User user, Game.GameType type)
    {
        switch (type)
        {
            case Game.GameType.None: return 0;
            case Game.GameType.Any:
                double result = statisticsService.WinFrequency(user.WinCount ?? 0, user.LossCount ?? 0);
                return result;
            default:
                int win = (await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: true)).Count;
                int loss = (await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: false)).Count;
                return statisticsService.WinFrequency(win, loss);
        }
    }

    public async Task<int> GetWinCountAsync(User user, Game.GameType type)
    {
        return type switch
        {
            Game.GameType.None => 0,
            Game.GameType.Any => user.WinCount ?? 0,
            _ => (await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: true)).Count,
        };
    }

    public async Task<int> GetLossCountAsync(User user, Game.GameType type)
    {
        return type switch
        {
            Game.GameType.None => 0,
            Game.GameType.Any => user.LossCount ?? 0,
            _ => (await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: false)).Count,
        };
    }

    public async Task<int> GetTotalCountAsync(User user, Game.GameType type) => await GetWinCountAsync(user, type) - await GetLossCountAsync(user, type);

    public async Task<double> GetWinBalanceAsync(User user, Game.GameType type)
    {
        switch (type)
        {
            case Game.GameType.None: return 0;
            case Game.GameType.Any: return user.WinBalance ?? 0;
            default:
                double win = 0;
                List<DataBaseClasses.Entity.Game> games = await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: true);
                foreach (DataBaseClasses.Entity.Game game in games) win += game.WinAmount ?? 0;
                return win;
        }
    }

    public async Task<double> GetLossBalanceAsync(User user, Game.GameType type)
    {
        switch (type)
        {
            case Game.GameType.None: return 0;
            case Game.GameType.Any: return user.LossBalance ?? 0;
            default:
                double win = 0;
                List<DataBaseClasses.Entity.Game> games = await gameRepository.GetGamesAsync(user.Id, (int)type, isWin: false);
                foreach (DataBaseClasses.Entity.Game game in games) win += game.Bid ?? 0;
                return win;
        }
    }
    public async Task<double> GetTotalBalanceAsync(User user, Game.GameType type) => await GetWinBalanceAsync(user, type) - await GetLossBalanceAsync(user, type);
}
