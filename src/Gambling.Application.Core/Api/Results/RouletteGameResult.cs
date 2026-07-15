namespace Gambling.Application.Core.Api.Results;

public record class RouletteGameResult(bool Result, string Message, double Win, int WinElement) : GameResult(Result, Message, Win);
