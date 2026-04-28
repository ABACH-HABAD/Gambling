namespace BusinessLogic.Captcha;

public abstract class BaseCaptchaService : ICaptchaService
{
    protected int amountOfAttempts;
    protected ICaptchaView? captcha;

    public int AmountOfAttempts { get => amountOfAttempts; }

    public void InitializationChaptcha(ICaptchaView element, int amountOfAttempts)
    {
        this.captcha = element;
        this.amountOfAttempts = amountOfAttempts;
    }

    public abstract void GenerateChaptcha(int lenght, CaptchaPattern pattern);

    public bool CaptchaVerification(string answer)
    {
        if (captcha is null) throw new CaptchaDidNotInitializedException();
        if (captcha.Text is null || captcha.Text == string.Empty) throw new CaptchaDidNotGeneratedException();

        if (amountOfAttempts <= 0)
        {
            return false;
        }

        if (answer == captcha.Text)
        {
            return true;
        }
        else
        {
            amountOfAttempts--;
            return false;
        }
    }
}
