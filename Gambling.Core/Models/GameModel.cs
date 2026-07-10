namespace Gambling.Core.Models;

public class GameModel : BaseModel
{
    public int PlayerId { get; set; }
    public double Bet { get; set; }
    public bool IsWin { get; set; }
    public double WinAmount { get; set; }

    public UserModel? Player { get; set; }
    public GameTypeModel? GameType { get; set; }

    public string DisplayGameType => GameType?.Name ?? string.Empty;
    public string DisplayIsWin => IsWin ? "Выигрышь" : "Проигрышь";
}
