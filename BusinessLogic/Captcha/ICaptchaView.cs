namespace BusinessLogic.Captcha;

public interface ICaptchaView
{
    public string Text { get; }
    public void GenerateCaptcha(params object[] parametrs);
}
