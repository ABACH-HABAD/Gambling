using BusinessLogic.Captcha;
using System.Windows.Controls;

namespace GamblingWpfUser.CaptchaAdapters;

internal class SimpleCaptchaAdapter(TextBlock textBlock) :  ICaptchaView
{
    public  string Text { get => textBlock.Text; }

    public void GenerateCaptcha(params object[] parametrs)
    {
        if (parametrs.Length < 1) throw new CaptchaException();
        if (parametrs[0] is not string text) throw new CaptchaException();

       textBlock.Text = text;
    }
}
