namespace BusinessLogic.Game.Slots;

public record SlotGameResult(bool Result, string Message, double Win, List<List<SlotElement>> Elements) : GameResult(Result, Message, Win);
