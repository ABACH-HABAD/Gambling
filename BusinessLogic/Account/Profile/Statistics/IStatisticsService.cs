namespace BusinessLogic.Account.Profile.Statistics;

public interface IStatisticsService
{
    public double WinFrequency(int winCount, int lossCount);
}
