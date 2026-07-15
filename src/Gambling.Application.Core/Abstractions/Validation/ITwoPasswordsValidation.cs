namespace Gambling.Application.Core.Abstractions.Validation;

public interface ITwoPasswordsValidation : IValidation
{
    public bool ValidateTwoPasswords(string firstPassword, string secondPassword, out string error);
}
