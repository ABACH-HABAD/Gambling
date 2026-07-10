namespace Gambling.Application.Core.Api.Results;

public record BlackjackGameResult(bool Result, string Message, double Win) : GameResult(Result, Message, Win);