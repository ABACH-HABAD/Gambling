namespace Gambling.Application.Core.Abstractions.Balance;

public interface IBalanceService
{
    public Task<bool> AddToBalanceAsync(int userId, double count);
    public Task<bool> RemoveFromBalanceAsync(int userId, double count);
}