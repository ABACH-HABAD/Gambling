using Gambling.Core.Models;

namespace Gambling.Application.Core.Abstractions.Sessions;

public interface ISessionStorageService
{
    public Task<List<SessionModel>> GetAllSessionsAsync();
}