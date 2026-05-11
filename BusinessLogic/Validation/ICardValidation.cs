using BusinessLogic.Account.Balance;

namespace BusinessLogic.Validation;

public interface ICardValidation : IValidation
{
    public bool CardValidation(PayCard card, out string error);
}
