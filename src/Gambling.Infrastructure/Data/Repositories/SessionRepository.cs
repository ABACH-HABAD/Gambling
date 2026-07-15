using Microsoft.EntityFrameworkCore;
using Gambling.Infrastructure.Data;
using Gambling.Infrastructure.Data.Projections;
using Gambling.Infrastructure.Data.Entities;
using Gambling.Core.Models;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Exceptions;

namespace Gambling.Infrastructure.Data.Repositories;

public class SessionRepository(ApplicationContext dataBaseContext) : BaseRepository(dataBaseContext), ISessionRepository
{
    public async Task<SessionModel?> GetWithIdAsync(int id)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        SessionModel? sessionModel = await _dataBaseContext.Sessions
        .ToSessionModel()
        .FirstOrDefaultAsync(session => session.Id == id);

        return sessionModel;
    }

    public async Task<List<SessionModel>> GetAllSessionsAsync()
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        List<SessionModel> list = await _dataBaseContext.Sessions
        .ToSessionModel()
        .OrderBy(session => session.IsComplete)
        .ToListAsync();

        return list;
    }

    public async Task<(SessionModel session, string token)> FindByRefreshTokenAsync(string refreshToken, string? ip)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        SessionEntity? session = await _dataBaseContext.Sessions
        .FirstOrDefaultAsync(sessionEntity => sessionEntity.Ip == ip && sessionEntity.RefreshToken == refreshToken);

        if (session != null)
        {
            return (session.ToModel(), session.RefreshToken);
        }
        else throw new SessionNotFoundException();
    }

    public async Task RecordLoginAsync(UserModel user, int deviceType, string refreshToken, string? ip)
    {
        if (!CheckConncetion()) throw new NoConnectionException();
        ArgumentNullException.ThrowIfNull(user);

        SessionEntity session = new()
        {
            UserId = user.Id,
            Time = DateTime.Now,
            DeviceTypeId = deviceType,
            IsComplete = false,
            RefreshToken = refreshToken
        };
        if (ip != null) session.Ip = ip;

        await _dataBaseContext.Sessions.AddAsync(session);
        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task<(bool status, int userId)> CheckActiveSessionAsync(string refreshToken, int deviceType, string? ip)
    {
        if (!CheckConncetion()) throw new NoConnectionException();
        if (refreshToken == null || refreshToken == string.Empty) return (false, 0);

        if (await _dataBaseContext.Sessions
            .Where(session =>
            session.RefreshToken == refreshToken &&
            session.DeviceTypeId == deviceType &&
            session.IsComplete == false &&
            session.Ip == ip)
            .Include(session => session.DeviceType)
            .OrderByDescending(session => session.Time)
            .FirstOrDefaultAsync() is SessionEntity findSession)
        {
            if (findSession.Time is DateTime lastLoginTime)
            {
                if (lastLoginTime.AddDays(30) > DateTime.Now)
                {
                    if (findSession.UserId is int userId) return (true, userId);
                    else return (false, 0);
                }
                else return (false, 0);
            }
            else return (false, 0);
        }
        else return (false, 0);
    }

    public async Task CompleteSessionAsync(string refreshToken, int deviceType, string? ip)
    {
        if (!CheckConncetion()) throw new NoConnectionException();
        ArgumentNullException.ThrowIfNull(refreshToken);

        if (await _dataBaseContext.Sessions
            .Where(session =>
            session.RefreshToken == refreshToken &&
            session.DeviceTypeId == deviceType &&
            session.IsComplete == false &&
            session.Ip == ip)
            .OrderByDescending(session => session.Time)
            .Include(session => session.DeviceType)
            .FirstOrDefaultAsync() is SessionEntity findSession)
        {
            findSession.IsComplete = true;
        }

        await _dataBaseContext.SaveChangesAsync();
    }
}