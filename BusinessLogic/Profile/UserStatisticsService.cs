using DataBaseClasses.Entity;

namespace BusinessLogic.Profile;

public class UserStatisticsService(IStatisticsService statisticsService) : IUserStatisticsService 
{
    public double WinFrequency(User user)
    {
        return statisticsService.WinFrequency(user.WinCount ?? 0, user.LossCount ?? 0);
    }
}
