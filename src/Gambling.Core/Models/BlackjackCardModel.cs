namespace Gambling.Core.Models;

public class BlackjackCardModel : BaseModel
{
    public int GameId { get; set; }
    public string Denomination { get; set; } = string.Empty;
    public char Suit { get; set; }
    public bool InPlayerHand { get; set; }
    public bool IsOpen { get; set; }

    public BlackjackGameModel? BlackjackGame { get; set; }
}