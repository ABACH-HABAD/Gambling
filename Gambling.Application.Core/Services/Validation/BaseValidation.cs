using Gambling.Application.Core.Abstractions.Validation;

namespace Gambling.Application.Core.Services.Validation;

public abstract class BaseValidation : IValidation
{
    public abstract bool Validate(string input, out string error);
}
