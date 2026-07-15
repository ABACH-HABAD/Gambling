using Gambling.Application.Core.Api.Results;

namespace Gambling.Application.Core.Abstractions.PromtionCodes;

public interface IPromotionalCodeActivatorService
{
    public Task<PromocionalCodeActivateResult> ActivatePromocodeAsync(int userId, string code, double replenishment);
}