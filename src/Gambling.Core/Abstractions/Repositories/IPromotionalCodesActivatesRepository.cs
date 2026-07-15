using Gambling.Core.Models;

namespace Gambling.Core.Abstractions.Repositories;

public interface IPromotionalCodesActivatesRepository : IRepository
{
    public Task AddActivateAsync(PromotionalCodesActivateModel promotionalCodesActivate);
    public Task AddActivateAsync(int userId, int promocodeId);
}