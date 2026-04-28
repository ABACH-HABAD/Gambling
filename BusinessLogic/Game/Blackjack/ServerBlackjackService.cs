namespace BusinessLogic.Game.Blackjack;

public class ServerBlackjackService : IBlackjackService, IGameService
{
    private readonly Deck deck = new(mix: true);

    private readonly BlackjackPlayer dealer = new();
    private readonly BlackjackPlayer player = new();

    public void AddCard(BlackjackPlayer player)
    {
        player.Cards.Add(deck.NextCard());
    }

    public int Scores(List<Card> cards)
    {
        int scores = 0;
        int aces = 0;
        foreach (Card card in cards)
        {
            if (int.TryParse(card.Denomination, out int denomination)) scores += denomination;
            else if (card.Denomination == "A") aces++;
            else
            {
                scores += card.Denomination switch
                {
                    "J" => 10,
                    "Q" => 10,
                    "K" => 10,
                    _ => throw new Exception("Номинал не найден")
                };
            }
        }

        for (int i = 0; i < aces; i++)
        {
            if (scores > 21) scores++;
            else
            {
                if (scores + 11 <= 21 && aces == 1)
                {
                    scores += 11;
                }
                else scores++;
            }

            aces--;
        }

        return scores;
    }
}
