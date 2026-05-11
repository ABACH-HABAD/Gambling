using DataBaseClasses.Entity;

namespace BusinessLogic.Account.Profile.Statistics;

public interface IUserStatisticsService
{
    public Task<UserStatisticResult> GetUserStatisticResultAsync(int userId, Game.GameType type);

    public Task<double> GetWinFrequencyAsync(int userId, Game.GameType type);
    public Task<int> GetWinCountAsync(int userId, Game.GameType type);
    public Task<int> GetLossCountAsync(int userId, Game.GameType type);
    public Task<int> GetTotalCountAsync(int userId, Game.GameType type);
    public Task<double> GetWinBalanceAsync(int userId, Game.GameType type);
    public Task<double> GetLossBalanceAsync(int userId, Game.GameType type);
    public Task<double> GetTotalBalanceAsync(int userId, Game.GameType type);

    public Task<UserStatisticResult> GetUserStatisticResultAsync(User user, Game.GameType type);

    public Task<double> GetWinFrequencyAsync(User user, Game.GameType type);
    public Task<int> GetWinCountAsync(User user, Game.GameType type);
    public Task<int> GetLossCountAsync(User user, Game.GameType type);
    public Task<int> GetTotalCountAsync(User user, Game.GameType type);
    public Task<double> GetWinBalanceAsync(User user, Game.GameType type);
    public Task<double> GetLossBalanceAsync(User user, Game.GameType type);
    public Task<double> GetTotalBalanceAsync(User user, Game.GameType type);
}