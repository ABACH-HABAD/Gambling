namespace Gambling.Application.Core.Api.Results;

public record UserStatisticResult(double WinFrequency, int WinCount, int LossCount, int TotalCount, double WinBalance, double LossBalance, double TotalBalance);