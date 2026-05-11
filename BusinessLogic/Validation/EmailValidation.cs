using System.Text.RegularExpressions;

namespace BusinessLogic.Validation;

public partial class EmailValidation : BaseValidation, IEmailValidation
{
    protected const string EMAIL_PATTERN = @"\w*@\w*\.\w{2,4}$";

    [GeneratedRegex(EMAIL_PATTERN)]
    protected static partial Regex EmailRegex();

    public override bool Validate(string input, out string error)
    {
        error = string.Empty;

        if (input == null || input.Length == 0 || input == string.Empty)
        {
            error = "Введите логин";
            return false;
        }

        if (!EmailRegex().IsMatch(input))
        {
            error = "Логин должен быть настоящей электронной почтой";
            return false;
        }

        return true;
    }
}
