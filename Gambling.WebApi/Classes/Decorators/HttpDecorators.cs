using Google.Protobuf;

namespace Gambling.WebApi.Classes.Decorators;

public static class HttpDecorators
{
    public const string Dot = ". ";
    public const string GetRequest = "Получен запрос ";
    public const string WithMethod = " с методом: ";
    public const string WithIp = " с IP: ";
    public const string ResponceDone = "Запрос обработан, ";
    public const string Code = "код: ";

    public static string DecorateRequest(string message)
    {
        return GetRequest + message;
    }

    public static string DecorateMethod(string message)
    {
        return WithMethod + message;
    }

    public static string DecorateIp(string message)
    {
        return WithIp + message;
    }

    public static string DecorateDone(string message)
    {
        return ResponceDone + message;
    }
    public static string DecorateDone(int code)
    {
        return DecorateDone(Code + code + Dot);
    }
}