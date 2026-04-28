namespace BusinessLogic.Game.Blackjack;

public class Deck
{
    private readonly Random random = new();
    private Queue<Card> Cards { get; init; } = [];

    public Deck(bool mix = false)
    {
        foreach (string denomination in Card.DenominationVariants)
        {
            foreach (char suit in Card.SuitVariants)
            {
                Cards.Enqueue(new Card(denomination, suit));
            }
        }

        if (mix) Shuffle();
    }

    public Card NextCard() => Cards.Dequeue();

    public void Shuffle()
    {
        Card[] cards = [.. Cards];

        for (int i = cards.Length; i > 0; i--)
        {
            int j = random.Next(0, cards.Length);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }

        Cards.Clear();

        for (int i = cards.Length; i > 0; i--)
        {
            Cards.Enqueue(cards[i]);
        }
    }
}
