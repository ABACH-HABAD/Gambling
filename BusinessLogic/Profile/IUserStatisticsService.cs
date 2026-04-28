using DataBaseClasses.Entity;

namespace BusinessLogic.Profile;

public interface IUserStatisticsService
{
    public double WinFrequency(User user);
}
