using BusinessLogic.Validation;
using System.Text.RegularExpressions;

namespace BusinessLogic.Auth;

public abstract partial class BaseValidation : IValidation
{
    protected const string EMAIL_PATTERN = @"\w*@\w*\.\w{2,4}$";


    [GeneratedRegex(EMAIL_PATTERN)]
    protected static partial Regex MyRegex();


    public abstract bool Validate(string input, out string error);
}
