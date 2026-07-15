namespace Gambling.Application.Core.Abstractions.Validation;

public interface IValidation
{
    public bool Validate(string input, out string error);
}
