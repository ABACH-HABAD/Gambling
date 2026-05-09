using BusinessLogic.Auth;
namespace BusinessLogic.Validation;

public class NameValidationService : BaseValidation, INameValidation
{
    public override bool Validate(string input, out string error)
    {
        if (input == string.Empty || input == null)
        {
            error = "Имя не может пустым";
            return false;
        }
        if (input.Length > 45)
        {
            error = "Имя не может быть длиннее 45 символов";
            return false;
        }

        error = string.Empty;
        return true;
    }
}
