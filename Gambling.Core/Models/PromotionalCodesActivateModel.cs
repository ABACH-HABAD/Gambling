namespace Gambling.Core.Models;

public class PromotionalCodesActivateModel : BaseModel
{
    public int PromotionalCodeId { get; set; }
    public int UserId { get; set; }

    public UserModel? User { get; set; }
}