namespace BusinessLogic.Game.Blackjack;

public record BlackjackGameResult(bool Result, string Message, double Win) : GameResult(Result, Message, Win);