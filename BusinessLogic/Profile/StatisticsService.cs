namespace BusinessLogic.Profile;

public class StatisticsService : IStatisticsService
{
    public double WinFrequency(int winCount, int lossCount)
    {
        int totalGames = winCount + lossCount;
        if (totalGames > 0) return winCount / totalGames;
        return 0;
    }
}
