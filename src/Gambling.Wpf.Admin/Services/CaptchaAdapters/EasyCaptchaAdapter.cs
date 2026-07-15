using EasyCaptcha.Wpf;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Core.Exceptions;

namespace Gambling.Wpf.Admin.Services.CaptchaAdapters;

internal class EasyCaptchaAdapter(Captcha easyCaptcha) : ICaptchaView
{
    public string Text { get => easyCaptcha.CaptchaText; }

    public void GenerateCaptcha(params object[] parametrs)
    {
        if (parametrs.Length < 2) throw new CaptchaException();
        if (parametrs[0] is not Captcha.LetterOption letterOption) throw new CaptchaException();
        if (parametrs[1] is not int numbers) throw new CaptchaException();

        easyCaptcha.CreateCaptcha(letterOption, numbers);
    }
}
