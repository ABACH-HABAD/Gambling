namespace BusinessLogic.Auth.Validation;

public class PasswordValidation : BaseValidation, IValidation, ITwoPasswordsValidation
{
    public override bool Validate(string input, out string error)
    {
        error = string.Empty;

        if (input == null || input.Length == 0 || input == string.Empty)
        {
            error = "Введите пароль";
            return false;
        }

        if (input.Length < 8)
        {
            error = "Пароль слишком короткий";
            return false;
        }

        if (input.Length > 64)
        {
            error = "Пароль слишком длинный";
            return false;
        }

        return true;
    }

    public bool ValidateTwoPasswords(string firstPassword, string secondPassword, out string error)
    {
        error = string.Empty;

        if (Validate(firstPassword, out string firstPasswordError))
        {
            if (firstPassword.Equals(secondPassword))
            {
                return true;
            }
            else
            {
                error = "Пароли не совпадают";
                return false;
            }
        }
        else
        {
            error = firstPasswordError;
            return false;
        }
    }
}
