using DataBaseClasses.Entity;

namespace BusinessLogic.Profile.Statistics;

public interface IUserStatisticsService
{
    public Task<double> WinFrequency(int userId, Game.GameType type);
    public Task<int> WinCount(int userId, Game.GameType type);
    public Task<int> LossCount(int userId, Game.GameType type);
    public Task<int> TotalCount(int userId, Game.GameType type);
    public Task<double> WinBalance(int userId, Game.GameType type);
    public Task<double> LossBalance(int userId, Game.GameType type);
    public Task<double> TotalBalance(int userId, Game.GameType type);


    public Task<double> WinFrequency(User user, Game.GameType type);
    public Task<int> WinCount(User user, Game.GameType type);
    public Task<int> LossCount(User user, Game.GameType type);
    public Task<int> TotalCount(User user, Game.GameType type);
    public Task<double> WinBalance(User user, Game.GameType type);
    public Task<double> LossBalance(User user, Game.GameType type);
    public Task<double> TotalBalance(User user, Game.GameType type);
}