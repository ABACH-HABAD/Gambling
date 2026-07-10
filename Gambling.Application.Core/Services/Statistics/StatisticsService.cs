using Gambling.Application.Core.Abstractions.Statistics;

namespace Gambling.Application.Core.Services.Statistics;

public class StatisticsService : IStatisticsService
{
    public double WinFrequency(int winCount, int lossCount)
    {
        int totalGames = winCount + lossCount;
        if (totalGames > 0) 
            return (double)((double)winCount / (double)totalGames);
        return 0;
    }
}