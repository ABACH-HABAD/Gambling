namespace BusinessLogic.Captcha;

public enum CaptchaPattern
{
    OnlyNumbers,
    OnlyLetters,
    NumbersAndLetters
}

public interface ICaptchaService
{
    public int AmountOfAttempts { get; }
    public void InitializationChaptcha(ICaptchaView element, int amountOfAttempts);
    public void GenerateChaptcha(int lenght, CaptchaPattern pattern);
    public bool CaptchaVerification(string answer);
}

public class CaptchaException : Exception;
public class CaptchaDidNotGeneratedException : CaptchaException;
public class CaptchaDidNotInitializedException : CaptchaException;
