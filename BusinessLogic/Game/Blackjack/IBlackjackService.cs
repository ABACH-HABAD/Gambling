namespace BusinessLogic.Game.Blackjack;

public interface IBlackjackService
{
    public void AddCard(BlackjackPlayer player);
    public int Scores(List<Card> cards);
}
