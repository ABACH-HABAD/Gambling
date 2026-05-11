using BusinessLogic.Validation;
using DataBaseClasses.Repository.Interfaces;

namespace BusinessLogic.Account.Balance;

public class ServerBalanceService(ICardValidation cardValidation, IUserRepository userRepository, IPromotionalCodesRepository promotionalCodesRepository) : ICardPayService, IBalanceService
{
    //Для внутренних операций
    public async Task<bool> AddToBalanceAsync(int userId, double count)
    {
        try
        {
            userRepository.AddToBalance(userId, count);
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
            userRepository.WriteOffFromBalance(userId, count);
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
            if (promocode != null) count = promotionalCodesRepository.ActivateCode(promocode, count);
            try
            {
                userRepository.AddToBalance(userId, count);
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
                userRepository.WriteOffFromBalance(userId, count);
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
