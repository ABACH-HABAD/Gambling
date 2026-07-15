using Gambling.Application.Core.BusinessModels;

namespace Gambling.Application.Core.Abstractions.Validation;

public interface ICardValidation : IValidation
{
    public bool CardValidation(PayCard card, out string error);
}
