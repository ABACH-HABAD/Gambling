using Gambling.Application.Core.Api.Results;
using Gambling.Core.Models;

namespace Gambling.Application.Core.Abstractions.Statistics;

public interface IUserStatisticsService
{
    public Task<UserStatisticResult> GetUserStatisticResultAsync(int userId, GameType type);

    public Task<double> GetWinFrequencyAsync(int userId, GameType type);
    public Task<int> GetWinCountAsync(int userId, GameType type);
    public Task<int> GetLossCountAsync(int userId, GameType type);
    public Task<int> GetTotalCountAsync(int userId, GameType type);
    public Task<double> GetWinBalanceAsync(int userId, GameType type);
    public Task<double> GetLossBalanceAsync(int userId, GameType type);
    public Task<double> GetTotalBalanceAsync(int userId, GameType type);

    public Task<UserStatisticResult> GetUserStatisticResultAsync(UserModel user, GameType type);

    public Task<double> GetWinFrequencyAsync(UserModel user, GameType type);
    public Task<int> GetWinCountAsync(UserModel user, GameType type);
    public Task<int> GetLossCountAsync(UserModel user, GameType type);
    public Task<int> GetTotalCountAsync(UserModel user, GameType type);
    public Task<double> GetWinBalanceAsync(UserModel user, GameType type);
    public Task<double> GetLossBalanceAsync(UserModel user, GameType type);
    public Task<double> GetTotalBalanceAsync(UserModel user, GameType type);
}