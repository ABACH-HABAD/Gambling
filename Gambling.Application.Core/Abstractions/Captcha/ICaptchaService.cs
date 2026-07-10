namespace Gambling.Application.Core.Abstractions.Captcha;

public interface ICaptchaService
{
    public int AmountOfAttempts { get; }
    public void InitializationChaptcha(ICaptchaView element, int amountOfAttempts);
    public void GenerateChaptcha(int lenght, CaptchaPattern pattern);
    public bool CaptchaVerification(string answer);
}