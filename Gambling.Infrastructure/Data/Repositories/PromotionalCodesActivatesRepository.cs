using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Models;
using Gambling.Infrastructure.Data;
using Gambling.Infrastructure.Data.Entities;

namespace Gambling.Infrastructure.Data.Repositories;

public class PromotionalCodesActivatesRepository(ApplicationContext context) : BaseRepository(context), IPromotionalCodesActivatesRepository
{
    private async Task AddActivateAsync(PromotionalCodesActivateEntity promotionalCodesActivate)
    {
        await _dataBaseContext.Promotional_codes_activates.AddAsync(promotionalCodesActivate);
        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task AddActivateAsync(PromotionalCodesActivateModel promotionalCodesActivate) => await AddActivateAsync(promotionalCodesActivate.UserId, promotionalCodesActivate.PromotionalCodeId);
    public async Task AddActivateAsync(int userId, int promocodeId) => await AddActivateAsync(new PromotionalCodesActivateEntity() { UserId = userId, PromotionalCodeId = promocodeId });
}