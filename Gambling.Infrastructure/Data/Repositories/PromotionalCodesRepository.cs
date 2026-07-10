using Microsoft.EntityFrameworkCore;
using Gambling.Infrastructure.Data;
using Gambling.Infrastructure.Data.Projections;
using Gambling.Infrastructure.Data.Entities;
using Gambling.Core.Models;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Exceptions;

namespace Gambling.Infrastructure.Data.Repositories;

public class PromotionalCodesRepository(ApplicationContext context) : BaseRepository(context), IPromotionalCodesRepository
{
    public async Task AddCodeAsync(string code, int use, int interest, int quantitative, int freespins)
    {
        await _dataBaseContext.Promotional_codes.AddAsync(new PromotionalCodeEntity()
        {
            Code = code,
            Use = use,
            InterestBonus = interest,
            QuantitativeBonus = quantitative,
            FreeSpins = freespins
        });

        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task<List<PromotionalCodeModel>> GetAllCodesAsync()
    {
        List<PromotionalCodeModel> codes = await _dataBaseContext.Promotional_codes
        .ToPromotionalCodeModel()
        .OrderByDescending(code => code.Use)
        .ToListAsync();

        return codes;
    }

    public async Task<int?> GetCodeIdAsync(string code)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        return (await _dataBaseContext.Promotional_codes.FirstOrDefaultAsync(promo => promo.Code == code))?.Id;
    }

    public async Task<double> GetBalanceChangeAfterActivationAsync(string code, double replenishment)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        PromotionalCodeEntity? usedCode = await _dataBaseContext.Promotional_codes.FirstOrDefaultAsync(promo => promo.Code == code);

        if (usedCode != null)
        {
            if (usedCode.Use > 0)
            {
                replenishment += usedCode.QuantitativeBonus;
                replenishment *= 1 + usedCode.InterestBonus;
            }
        }

        return replenishment;
    }

    public async Task<int> GetFreeSpinsCountAfterActivationAsync(string code)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        PromotionalCodeEntity? usedCode = await _dataBaseContext.Promotional_codes.FirstOrDefaultAsync(promo => promo.Code == code);

        if (usedCode != null)
        {
            if (usedCode.Use > 0)
            {
                return usedCode.FreeSpins;
            }
        }

        return 0;
    }

    public async Task SpendCodeUseAsync(string code)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        PromotionalCodeEntity? usedCode = await _dataBaseContext.Promotional_codes.FirstOrDefaultAsync(promo => promo.Code == code);

        if (usedCode != null)
        {
            if (usedCode.Use > 0) usedCode.Use--;
            else throw new CannotActivatePromotionalCodeException();
        }
        else throw new CannotActivatePromotionalCodeException();

        await _dataBaseContext.SaveChangesAsync();
    }
}