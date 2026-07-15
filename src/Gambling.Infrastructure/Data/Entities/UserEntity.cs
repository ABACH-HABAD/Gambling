using Gambling.Core.Abstractions.Models;
using Gambling.Core.Models;

namespace Gambling.Infrastructure.Data.Entities;

internal class UserEntity : BaseEntity<UserEntity, UserModel>, INameable
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public bool IsBlocked { get; set; }
    public double Coefficient { get; set; }
    public double Balance { get; set; }
    public int WinCount { get; set; }
    public int LossCount { get; set; }
    public double WinBalance { get; set; }
    public double LossBalance { get; set; }
    public int SlotsBonusCount { get; set; }

    public UserStatusEntity? Status { get; set; }

    internal override UserModel ToModel()
    {
        UserModel dto = new()
        {
            Id = Id,
            Email = Email,
            Name = Name,
            IsBlocked = IsBlocked,
            Coefficient = Coefficient,
            Balance = Balance,
            WinCount = WinCount,
            LossCount = LossCount,
            WinBalance = WinBalance,
            LossBalance = LossBalance,
            SlotsBonusCount = SlotsBonusCount

        };
        if (Status != null) dto.Status = Status.ToModel();
        return dto;
    }
}