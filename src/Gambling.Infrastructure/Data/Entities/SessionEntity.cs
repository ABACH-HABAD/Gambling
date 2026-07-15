using Gambling.Core.Models;

namespace Gambling.Infrastructure.Data.Entities;

internal class SessionEntity : BaseEntity<SessionEntity, SessionModel>
{
    public int UserId { get; set; }
    public DateTime Time { get; set; }
    public int DeviceTypeId { get; set; }
    public string Ip { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public bool IsComplete { get; set; }

    public UserEntity? User { get; set; }
    public DeviceTypeEntity? DeviceType { get; set; }

    internal override SessionModel ToModel()
    {
        SessionModel dto = new()
        {
            Id = Id,
            UserId = UserId,
            Time = Time,
            Ip = Ip,
            IsComplete = IsComplete
        };
        if (User != null) dto.User = User.ToModel();
        if (DeviceType != null) dto.DeviceType = DeviceType.ToModel();
        return dto;
    }
}