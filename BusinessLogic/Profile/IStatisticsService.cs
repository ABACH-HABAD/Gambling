namespace BusinessLogic.Profile;

public interface IStatisticsService
{
    public double WinFrequency(int winCount, int lossCount);
}
