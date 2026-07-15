using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Core.Exceptions;

namespace Gambling.Application.Client.Services.Captcha;

public class EasyCaptchaService : BaseCaptchaService, ICaptchaService
{
    public override void GenerateChaptcha(int lenght, CaptchaPattern pattern)
    {
        if (captcha is null) throw new CaptchaDidNotInitializedException();

        EasyCaptcha.Wpf.Captcha.LetterOption option = pattern switch
        {
            CaptchaPattern.OnlyNumbers => EasyCaptcha.Wpf.Captcha.LetterOption.Number,
            CaptchaPattern.OnlyLetters => EasyCaptcha.Wpf.Captcha.LetterOption.Number,
            CaptchaPattern.NumbersAndLetters => EasyCaptcha.Wpf.Captcha.LetterOption.Alphanumeric,
            _ => throw new CaptchaException()
        };

        captcha.GenerateCaptcha(option, lenght);
    }
}
