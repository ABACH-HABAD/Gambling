namespace Gambling.WebApi.Classes.Decorators;

public static class DateTimeStringDecorators
{
    public static string DecorateTime(string message, DateTime dateTime)
    {
        return $"[{dateTime}] {message}";
    }

    public static string DecorateNow(string message)
    {
        return DecorateTime(message, DateTime.Now);
    }
}