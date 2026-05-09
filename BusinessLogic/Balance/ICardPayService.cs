namespace BusinessLogic.Balance;

public interface ICardPayService
{
    public Task<bool> AddToBalanceAsync(int userId, double count, PayCard card, string? promocode = null);
    public Task<bool> RemoveFromBalanceAsync(int userId, double count, PayCard card);
}
