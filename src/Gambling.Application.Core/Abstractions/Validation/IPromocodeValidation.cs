namespace Gambling.Application.Core.Abstractions.Validation;

public interface IPromocodeValidation
{
    public bool Validate(string code, string uses, string interestBonus, string quantitativeBonus, string freeSpins, out string error);

    public bool Validate(string code, int uses, int interestBonus, int quantitativeBonus, int freeSpins, out string error);
}