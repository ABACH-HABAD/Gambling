using DataBaseClasses.Entity;

namespace DataBaseClasses.Repository.Interfaces;

public interface ISessionRepository : IRepository
{
    public Session? GetWithId(int id);
    public Session? FindByRefreshToken(string refreshToken, string? ip);
    public void RecordLogin(User user, int deviceType, string refreshToken, string? ip);
    public (bool status, int userId) CheckActiveSession(string refreshToken, int deviceType, string? ip);
    public void CompleteSession(string refreshToken, int deviceType, string? ip);
}
