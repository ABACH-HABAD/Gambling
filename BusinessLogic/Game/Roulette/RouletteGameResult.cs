namespace BusinessLogic.Game.Roulette;

public record class RouletteGameResult(bool Result, string Message, double Win, int WinElement) : GameResult(Result, Message, Win);
