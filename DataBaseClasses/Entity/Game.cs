namespace DataBaseClasses.Entity;

public class Game : Entity
{
    public int? Player { get; set; }
    public int? GameType { get; set; }
    public double? Bid { get; set; }
    public bool? IsWin { get; set; }
    public double? WinAmount { get; set; }
}
