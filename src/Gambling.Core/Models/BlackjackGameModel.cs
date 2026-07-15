namespace Gambling.Core.Models;

public class BlackjackGameModel : BaseModel
{
    public int UserId { get; set; }
    public double Bet { get; set; }
    public bool CanPlayerMove { get; set; }

    public List<BlackjackCardModel> Cards { get; set; } = [];
    public UserModel? User { get; set; }
}
