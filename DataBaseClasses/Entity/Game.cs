namespace DataBaseClasses.Entity;

public class Game : Entity
{
    public int? Player { get; set; }
    public int? GameType { get; set; }
    public double? Bid { get; set; }
    public bool? IsWin { get; set; }
    public double? WinAmount { get; set; }

    public string StringIsWin => ((IsWin ?? false) ? "Выигрышь" : "Проигрыш");

    public string StringGameType
    {
        get
        {
            return GameType switch
            {
                1 => "Слоты",
                2 => "Блекджек",
                3 => "Рулетка",
                _ => "Ошибка"
            };
        }
    }
}
