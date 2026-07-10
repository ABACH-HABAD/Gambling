namespace Gambling.WebApi.Classes.Decorators;

public static class ConsoleDecorators
{
    private static void ConsoleMessage(string message, ConsoleColor foregroundColor)
    {
        Console.ForegroundColor = foregroundColor;
        Console.Write(message);
    }

    private static void ConsoleEndLine()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }

    private static ConsoleColor StatusCodeColor(int statusCode)
    {
        if (statusCode >= 500) return ConsoleColor.DarkRed;
        else if (statusCode >= 400) return ConsoleColor.Red;
        else if (statusCode >= 300) return ConsoleColor.Cyan;
        else if (statusCode >= 200) return ConsoleColor.Green;
        else if (statusCode >= 100) return ConsoleColor.Gray;

        else throw new Exception("Получен недостижимый http код");
    }

    public static void RequesteDecoratorAsync(string path, string method, string ip)
    {
        ConsoleMessage(DateTimeStringDecorators.DecorateNow(string.Empty), ConsoleColor.Gray);

        ConsoleMessage(HttpDecorators.GetRequest, ConsoleColor.White);
        ConsoleMessage(path, ConsoleColor.Blue);
        ConsoleMessage(HttpDecorators.WithMethod, ConsoleColor.White);
        ConsoleMessage(method, ConsoleColor.Yellow);
        ConsoleMessage(HttpDecorators.WithIp, ConsoleColor.White);
        ConsoleMessage(ip, ConsoleColor.Blue);
        ConsoleMessage(HttpDecorators.Dot, ConsoleColor.White);

        ConsoleEndLine();
    }

    public static void ResponceDecoratorAsync(string path, string method, string ip, int code)
    {
        ConsoleMessage(DateTimeStringDecorators.DecorateNow(string.Empty), ConsoleColor.Gray);
        
        ConsoleMessage(HttpDecorators.ResponceDone, ConsoleColor.White);
        ConsoleMessage(path, ConsoleColor.Blue);
        ConsoleMessage(HttpDecorators.WithMethod, ConsoleColor.White);
        ConsoleMessage(method, ConsoleColor.Yellow);
        ConsoleMessage(HttpDecorators.WithIp, ConsoleColor.White);
        ConsoleMessage(ip, ConsoleColor.Blue);
        ConsoleMessage(HttpDecorators.Dot, ConsoleColor.White);
        
        ConsoleMessage(HttpDecorators.ResponceDone, ConsoleColor.Yellow);
        ConsoleMessage($"код: {code}", StatusCodeColor(code));
        ConsoleEndLine();
    }
}