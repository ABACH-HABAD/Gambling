using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Core.Exceptions;
using System.Text;

namespace Gambling.Application.Client.Services.Captcha;

public class SimpleCaptchaService : BaseCaptchaService, ICaptchaService
{

    private const string PATTERN_ONLY_LETTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string PATTERN_ONLY_NUMBERS = "0123456789";
    private const string PATTERN_LETTERS_AND_NUMBERS = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public override void GenerateChaptcha(int lenght, CaptchaPattern pattern)
    {
        if (captcha is null) throw new CaptchaDidNotInitializedException();

        string captchaText = pattern switch
        {
            CaptchaPattern.OnlyNumbers => SelectingCharacters(lenght, PATTERN_ONLY_NUMBERS),
            CaptchaPattern.OnlyLetters => SelectingCharacters(lenght, PATTERN_ONLY_LETTERS),
            CaptchaPattern.NumbersAndLetters => SelectingCharacters(lenght, PATTERN_LETTERS_AND_NUMBERS),
            _ => throw new CaptchaException()
        };

        captcha.GenerateCaptcha(captchaText);
    }

    private static string SelectingCharacters(int lenght, string pattern)
    {
        Random random = new();
        StringBuilder stringBuilder = new();

        for (int i = 0; i < lenght; i++)
        {
            stringBuilder.Append(pattern[random.Next(0, pattern.Length)]);
        }

        return stringBuilder.ToString();
    }
}
