namespace BusinessLogic.Validation;

public interface IValidation
{
    public bool Validate(string input, out string error);
}
