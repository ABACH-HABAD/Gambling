using DataBaseClasses.Entity;
using DataBaseClasses.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBaseClasses.Repository;

public class SessionRepository(ApplicationContext dataBaseContext) : BaseRepository(dataBaseContext), ISessionRepository
{
    public Session? GetWithId(int id)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        if (_dataBaseContext.Sessions.Find(id) is Session session) return session.Clone<Session>();
        else return null;
    }

    public Session? FindByRefreshToken(string refreshToken, string? ip)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        if (_dataBaseContext.Sessions.FirstOrDefault(session => session.RefreshToken == refreshToken && session.Ip == ip) is Session session) return session.Clone<Session>();
        else return null;
    }

    public void RecordLogin(User user, int deviceType, string refreshToken, string? ip)
    {
        if (!CheckConncetion()) throw new NoConnectionException();
        ArgumentNullException.ThrowIfNull(user);

        Session session = new()
        {
            User = user.Id,
            Time = DateTime.Now,
            DeviceType = deviceType,
            IsComplete = false,
            RefreshToken = refreshToken
        };
        if (ip != null) session.Ip = ip;

        _dataBaseContext.Sessions.Add(session);
        _dataBaseContext.SaveChanges();
    }

    public (bool status, int userId) CheckActiveSession(string refreshToken, int deviceType, string? ip)
    {
        if (!CheckConncetion()) throw new NoConnectionException();
        if (refreshToken == null || refreshToken == string.Empty) return (false, 0);

        if (_dataBaseContext.Sessions
            .Where(session => 
            session.RefreshToken == refreshToken &&
            session.DeviceType == deviceType &&
            session.IsComplete == false &&
            session.Ip == ip)
            .OrderByDescending(session => session.Time)
            .FirstOrDefault() is Session findSession)
        {
            if (findSession.Time is DateTime lastLoginTime)
            {
                if (lastLoginTime.AddDays(30) > DateTime.Now)
                {
                    if (findSession.User is int userId) return (true, userId);
                    else return (false, 0);
                }
                else return (false, 0);
            }
            else return (false, 0);
        }
        else return (false, 0);
    }

    public void CompleteSession(string refreshToken, int deviceType, string? ip)
    {
        if (!CheckConncetion()) throw new NoConnectionException();
        ArgumentNullException.ThrowIfNull(refreshToken);

        if (_dataBaseContext.Sessions
            .Where(session => 
            session.RefreshToken == refreshToken && 
            session.DeviceType == deviceType &&
            session.IsComplete == false && 
            session.Ip == ip)
            .OrderByDescending(session => session.Time)
            .FirstOrDefault() is Session findSession)
        {
            findSession.IsComplete = true;
        }

        _dataBaseContext.SaveChanges();
    }
}
