namespace BusinessLogic.Validation;

public abstract class BaseValidation : IValidation
{
    public abstract bool Validate(string input, out string error);
}
