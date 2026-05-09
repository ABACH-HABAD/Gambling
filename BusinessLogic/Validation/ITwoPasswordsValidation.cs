namespace BusinessLogic.Validation;

public interface ITwoPasswordsValidation
{
    public bool ValidateTwoPasswords(string firstPassword, string secondPassword, out string error);
}
