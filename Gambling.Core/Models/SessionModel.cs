namespace Gambling.Core.Models;

public class SessionModel : BaseModel
{
    public int UserId { get; set; }
    public DateTime Time { get; set; }
    public string Ip { get; set; } = string.Empty;
    public bool IsComplete { get; set; }

    public UserModel? User { get; set; }
    public DeviceTypeModel? DeviceType { get; set; }

    public string DisplayDeviceType => DeviceType?.Name ?? string.Empty;
    public string DisplayIsComplete => IsComplete ? "Завершена" : "Активная";
}