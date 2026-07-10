using Gambling.Core.Models;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Application.Core.Abstractions.PromtionCodes;
using Gambling.Application.Core.Abstractions.Validation;
using Gambling.Application.Core.Api.Results;

namespace Gambling.Application.Server.Services.Account.Balance.PromotionalCodes;

public class ServerPromotionalCodeService(
    IPromotionalCodesRepository promotionalCodesRepository,
    IPromotionalCodesActivatesRepository promotionalCodesActivatesRepository,
    IPromocodeValidation promocodeValidation) : IPromotionalCodeService, IPromotionalCodeActivatorService
{
    public async Task<PromocionalCodeActivateResult> ActivatePromocodeAsync(int userId, string code, double replenishment)
    {
        try
        {
            int codeId = await promotionalCodesRepository.GetCodeIdAsync(code) ?? throw new Exception("Промокод не найден");
            double balance = await promotionalCodesRepository.GetBalanceChangeAfterActivationAsync(code, replenishment);
            int freeSpins = await promotionalCodesRepository.GetFreeSpinsCountAfterActivationAsync(code);
            await promotionalCodesRepository.SpendCodeUseAsync(code);
            await promotionalCodesActivatesRepository.AddActivateAsync(userId, codeId);

            return new PromocionalCodeActivateResult(true, balance, freeSpins);
        }
        catch
        {
            return new PromocionalCodeActivateResult(false, 0, 0);
        }
    }
    public async Task<List<PromotionalCodeModel>> GetPromocodeListAsync()
    {
        List<PromotionalCodeModel> codes = await promotionalCodesRepository.GetAllCodesAsync();
        return codes;
    }

    public async Task AddPromocodeAsync(string code, int uses, int interestBonus, int quantitativeBonus, int freeSpins)
    {
        if (!promocodeValidation.Validate(code, uses, interestBonus, quantitativeBonus, freeSpins, out string error)) throw new Exception(error);

        await promotionalCodesRepository.AddCodeAsync(code, uses, interest: interestBonus, quantitative: quantitativeBonus, freespins: freeSpins);
    }
}