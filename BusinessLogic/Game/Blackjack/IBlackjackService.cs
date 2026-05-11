namespace BusinessLogic.Game.Blackjack;

public interface IBlackjackService : IGameService
{
    public Task<BlackjackGameState> FirstMove(int userId, double bet);
    public Task<BlackjackGameState> TakeCard(int userId);
    public Task<BlackjackGameState> TakeDouble(int userId);
    public Task<BlackjackGameState> Stand(int userId);
    public Task<BlackjackGameResult> EndGame(int userId);

    public int Scores(List<Card> cards);
}