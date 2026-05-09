namespace DataBaseClasses.Repository.Interfaces;

public interface IPromotionalCodesRepository
{
    public double ActivateCode(string code, double replenishment);
}
