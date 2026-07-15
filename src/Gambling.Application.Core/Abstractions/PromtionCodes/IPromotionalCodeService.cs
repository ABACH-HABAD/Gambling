using Gambling.Core.Models;

namespace Gambling.Application.Core.Abstractions.PromtionCodes;

public interface IPromotionalCodeService
{
    public Task<List<PromotionalCodeModel>> GetPromocodeListAsync();
    public Task AddPromocodeAsync(string code, int uses, int interestBonus, int quantitativeBonus, int freeSpins);
}