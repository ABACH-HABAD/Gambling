using Gambling.Core.Models;

namespace Gambling.Core.Abstractions.Repositories;

public interface ISessionRepository : IRepository
{
    public Task<SessionModel?> GetWithIdAsync(int id);

    public Task<List<SessionModel>> GetAllSessionsAsync();
    public Task<(SessionModel session, string token)> FindByRefreshTokenAsync(string refreshToken, string? ip);
    public Task RecordLoginAsync(UserModel user, int deviceType, string refreshToken, string? ip);
    public Task<(bool status, int userId)> CheckActiveSessionAsync(string refreshToken, int deviceType, string? ip);
    public Task CompleteSessionAsync(string refreshToken, int deviceType, string? ip);
}