using DataBaseClasses.Entity;
using DataBaseClasses.Repository.Interfaces;
using DataBaseClasses.Exceptions;

namespace DataBaseClasses.Repository;

public class PromotionalCodesRepository(ApplicationContext context) : BaseRepository(context), IPromotionalCodesRepository
{
    public double ActivateCode(string code, double replenishment)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        PromotionalCode? usedCode = _dataBaseContext.Promotional_codes.FirstOrDefault(promo => promo.Code == code);

        if (usedCode != null)
        {
            if (usedCode.Use > 0)
            {
                usedCode.Use--;
                replenishment += usedCode.QuantitativeBonus ?? 0;
                replenishment *= 1 + (usedCode.InterestBonus ?? 0);
            }
        }

        _dataBaseContext.SaveChanges();

        return replenishment;
    }
}
