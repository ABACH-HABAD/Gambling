namespace BusinessLogic.Account.Profile.Statistics;

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
