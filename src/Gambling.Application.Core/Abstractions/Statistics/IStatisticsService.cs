namespace Gambling.Application.Core.Abstractions.Statistics;

public interface IStatisticsService
{
    public double WinFrequency(int winCount, int lossCount);
}