namespace Gambling.Application.Core.Api.Results;

public abstract record class GameResult(bool Result, string Message, double Win);
