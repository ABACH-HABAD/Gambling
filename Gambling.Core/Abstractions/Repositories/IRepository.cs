namespace Gambling.Core.Abstractions.Repositories;

public interface IRepository
{
    public Task<bool> CheckConncetionAsync();
    public bool CheckConncetion();
}