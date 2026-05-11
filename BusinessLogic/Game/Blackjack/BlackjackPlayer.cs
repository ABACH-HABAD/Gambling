namespace BusinessLogic.Game.Blackjack;

public class BlackjackPlayer
{
    public List<Card> Cards { get; init;  } = [];

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }
}