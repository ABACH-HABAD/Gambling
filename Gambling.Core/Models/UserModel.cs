namespace Gambling.Core.Models;

public class UserModel : BaseModel
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
    public double Coefficient { get; set; }
    public double Balance { get; set; }
    public int WinCount { get; set; }
    public int LossCount { get; set; }
    public double WinBalance { get; set; }
    public double LossBalance { get; set; }
    public int SlotsBonusCount { get; set; }

    public UserStatusModel? Status { get; set; }

    public string DisplayRole => Status?.Name ?? string.Empty;
    public string DisplayIsBlocked => IsBlocked ? "Заблокирован" : "Разблокирован";
    public string DisplayIsBlockingAction => !IsBlocked ? "Заблокировать" : "Разблокировать";
}