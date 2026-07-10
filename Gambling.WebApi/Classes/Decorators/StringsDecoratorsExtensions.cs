namespace Gambling.WebApi.Classes.Decorators;

public static class StringsDecoratorsExtensions
{
    private const string NewLine = "\r\n";
    public static string DecorateTime(this string text, DateTime time)
    {
        return DateTimeStringDecorators.DecorateTime(text, time);
    }

    public static string DecorateNow(this string text)
    {
        return DateTimeStringDecorators.DecorateNow(text);
    }

    public static string DecorateNewLine(this string text)
    {
        return text += NewLine;
    }

    public static string DecorateRequest(this string message)
    {
        return HttpDecorators.DecorateRequest(message);
    }

    public static string DecorateMethod(this string message)
    {
        return HttpDecorators.DecorateMethod(message);
    }

    public static string DecorateIp(this string message)
    {
        return HttpDecorators.DecorateIp(message);
    }

    public static string DecorateDone(this string message)
    {
        return HttpDecorators.DecorateDone(message);
    }

    public static string DecorateDone(this int code)
    {
        return HttpDecorators.DecorateDone(code);
    }
}