using BusinessLogic.Auth;
using DataBaseClasses.Entity;
using DataBaseClasses.Repository.Interfaces;

namespace BusinessLogic.Profile.Statistics;

public class ServerUserStatisticsService(IStatisticsService statisticsService, IAccountService accountService, IGameRepository gameRepository) : IUserStatisticsService
{
    public async Task<double> WinFrequency(int userId, Game.GameType type)
    {
        User? user = await accountService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await WinFrequency(user, type);
    }

    public async Task<int> WinCount(int userId, Game.GameType type)
    {
        User? user = await accountService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await WinCount(user, type);
    }

    public async Task<int> LossCount(int userId, Game.GameType type)
    {
        User? user = await accountService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await LossCount(user, type);
    }

    public async Task<int> TotalCount(int userId, Game.GameType type)
    {
        User? user = await accountService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await TotalCount(user, type);
    }

    public async Task<double> WinBalance(int userId, Game.GameType type)
    {
        User? user = await accountService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await WinBalance(user, type);
    }

    public async Task<double> LossBalance(int userId, Game.GameType type)
    {
        User? user = await accountService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await LossBalance(user, type);
    }

    public async Task<double> TotalBalance(int userId, Game.GameType type)
    {
        User? user = await accountService.GetUserDataAsync(userId) ?? throw new ArgumentException("Пользователь с таким ID не найдке", nameof(userId));
        return await TotalBalance(user, type);
    }


    public async Task<double> WinFrequency(User user, Game.GameType type)
    {
        switch (type)
        {
            case Game.GameType.None: return 0;
            case Game.GameType.Any: 
                double result = statisticsService.WinFrequency(user.WinCount ?? 0, user.LossCount ?? 0);
                return result;
            default:
                int win = gameRepository.GetGames(user.Id, (int)type, isWin: true).Count;
                int loss = gameRepository.GetGames(user.Id, (int)type, isWin: false).Count;
                return statisticsService.WinFrequency(win, loss);
        }
    }

    public async Task<int> WinCount(User user, Game.GameType type)
    {
        return type switch
        {
            Game.GameType.None => 0,
            Game.GameType.Any => user.WinCount ?? 0,
            _ => gameRepository.GetGames(user.Id, (int)type, isWin: true).Count,
        };
    }

    public async Task<int> LossCount(User user, Game.GameType type)
    {
        return type switch
        {
            Game.GameType.None => 0,
            Game.GameType.Any => user.LossCount ?? 0,
            _ => gameRepository.GetGames(user.Id, (int)type, isWin: false).Count,
        };
    }

    public async Task<int> TotalCount(User user, Game.GameType type) => await WinCount(user, type) - await TotalCount(user, type);

    public async Task<double> WinBalance(User user, Game.GameType type)
    {
        switch (type)
        {
            case Game.GameType.None: return 0;
            case Game.GameType.Any: return user.WinBalance ?? 0;
            default:
                double win = 0;
                List<DataBaseClasses.Entity.Game> games = gameRepository.GetGames(user.Id, (int)type, isWin: true);
                foreach (DataBaseClasses.Entity.Game game in games) win += game.WinAmount ?? 0;
                return win;
        }
    }

    public async Task<double> LossBalance(User user, Game.GameType type)
    {
        switch (type)
        {
            case Game.GameType.None: return 0;
            case Game.GameType.Any: return user.LossBalance ?? 0;
            default:
                double win = 0;
                List<DataBaseClasses.Entity.Game> games = gameRepository.GetGames(user.Id, (int)type, isWin: false);
                foreach (DataBaseClasses.Entity.Game game in games) win += game.Bid ?? 0;
                return win;
        }
    }
    public async Task<double> TotalBalance(User user, Game.GameType type) => await WinBalance(user, type) - await LossBalance(user, type);

}
