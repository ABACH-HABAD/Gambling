using BusinessLogic.Auth;
using System.Text.RegularExpressions;

namespace BusinessLogic.Validation;

public class EmailValidation : BaseValidation, IValidation
{
    public override bool Validate(string input, out string error)
    {
        error = string.Empty;

        if (input == null || input.Length == 0 || input == string.Empty)
        {
            error = "Введите логин";
            return false;
        }

        if (!MyRegex().IsMatch(input))
        {
            error = "Логин должен быть настоящей электронной почтой";
            return false;
        }

        return true;
    }
}
