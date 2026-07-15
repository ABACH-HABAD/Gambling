using Gambling.Application.Core.Abstractions.Balance;
using Gambling.Application.Core.Abstractions.PromtionCodes;
using Gambling.Application.Core.Abstractions.Validation;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.BusinessModels;
using Gambling.Core.Abstractions.Repositories;

namespace Gambling.Application.Server.Services.Account.Balance;

public class ServerBalanceService(ICardValidation cardValidation, IUserRepository userRepository, IPromotionalCodeActivatorService promotionalCodeActivatorService) : ICardPayService, IBalanceService
{
    //Для внутренних операций
    public async Task<bool> AddToBalanceAsync(int userId, double count)
    {
        try
        {
            await userRepository.AddToBalanceAsync(userId, count);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveFromBalanceAsync(int userId, double count)
    {
        try
        {
            await   userRepository.WriteOffFromBalanceAsync(userId, count);
            return true;
        }
        catch
        {
            return false;
        }
    }

    //Для приёма оплаты по карте
    public async Task<bool> AddToBalanceAsync(int userId, double count, PayCard card, string? promocode = null)
    {
        if (cardValidation.CardValidation(card, out _))
        {
            if (promocode != null)
            {
                PromocionalCodeActivateResult result = await promotionalCodeActivatorService.ActivatePromocodeAsync(userId, promocode, count);
                if (result.IsActivateSucces)
                {
                    count = result.ChangeBalance;
                    await userRepository.ChangeSlotsBonusGamesCountAsync(userId, result.FreeSpinsChange);
                }
            }

            try
            {
                await userRepository.AddToBalanceAsync(userId, count);
                return true;
            }
            catch
            {
                return false;
            }
        }
        else return false;
    }

    public async Task<bool> RemoveFromBalanceAsync(int userId, double count, PayCard card)
    {
        if (cardValidation.CardValidation(card, out _))
        {
            try
            {
                await userRepository.WriteOffFromBalanceAsync(userId, count);
                return true;
            }
            catch
            {
                return false;
            }
        }
        else return false;
    }
}
