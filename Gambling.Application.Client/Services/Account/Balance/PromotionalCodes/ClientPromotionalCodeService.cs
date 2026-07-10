using Gambling.Application.Client.Services.ApiServices;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.PromtionCodes;
using Gambling.Application.Core.Abstractions.Validation;
using Gambling.Core.Models;

namespace Gambling.Application.Client.Services.Account.Balance.PromotionalCodes;

public class ClientPromotionalCodeService(IPromocodeValidation promocodeValidation, IApiClient apiClient) : IPromotionalCodeService
{
    public async Task AddPromocodeAsync(PromotionalCodeModel code)
    {
        HttpResponseMessage response;

        response = await apiClient.PostAsync("promocode/add", code);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Не удалось добавить промокод");
        }
    }

    public async Task AddPromocodeAsync(string code, int uses, int interestBonus, int quantitativeBonus, int freeSpins)
    {
        if (!promocodeValidation.Validate(code, uses, interestBonus, quantitativeBonus, freeSpins, out string error)) throw new Exception(error);

        await AddPromocodeAsync(new PromotionalCodeModel()
        {
            Id = 0,
            Code = code,
            Use = uses,
            InterestBonus = interestBonus,
            QuantitativeBonus = quantitativeBonus,
            FreeSpins = freeSpins
        });
    }

    public async Task<List<PromotionalCodeModel>> GetPromocodeListAsync()
    {
        HttpResponseMessage response;

        response = await apiClient.GetAsync("/promocode/getAll");

        if (response.IsSuccessStatusCode)
        {
            List<PromotionalCodeModel>? codes = await response.WithTypeAsync<List<PromotionalCodeModel>>();
            return codes ?? [];
        }
        else
        {
            throw new Exception("Не загрузить список промокодов");
        }
    }
}