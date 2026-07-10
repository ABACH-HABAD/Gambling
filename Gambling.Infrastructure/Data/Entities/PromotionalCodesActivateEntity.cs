using Gambling.Core.Models;

namespace Gambling.Infrastructure.Data.Entities;

internal class PromotionalCodesActivateEntity : BaseEntity<PromotionalCodesActivateEntity, PromotionalCodesActivateModel>
{
    public int PromotionalCodeId { get; set; }
    public int UserId { get; set; }

    public PromotionalCodeEntity? PromotionalCode { get; set; }
    public UserEntity? User { get; set; }

    internal override PromotionalCodesActivateModel ToModel()
    {
        PromotionalCodesActivateModel dto = new()
        {
            Id = Id,
            PromotionalCodeId = PromotionalCodeId,
            UserId = UserId
        };
        if (User != null) dto.User = User.ToModel();
        return dto;
    }
}