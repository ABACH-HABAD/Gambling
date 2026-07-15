namespace Gambling.Application.Core.BusinessModels.GameModels.Blackjack;

public class Deck
{

    private readonly Random random = new();
    private Queue<Card> Cards { get; init; } = [];

    public int Count => throw new NotImplementedException();

    public bool IsSynchronized => throw new NotImplementedException();

    public object SyncRoot => throw new NotImplementedException();

    public Deck(bool mix = false, bool isOpen = true)
    {
        foreach (string denomination in Card.DenominationVariants)
        {
            foreach (char suit in Card.SuitVariants)
            {
                Cards.Enqueue(new Card(denomination, suit, isOpen));
            }
        }

        if (mix)
            Shuffle();
    }

    public void RemoveCards(Func<Card, bool> selector)
    {
        Card[] cards = [.. Cards];

        for (int i = cards.Length - 1; i >= 0; i--)
        {
            if (selector(cards[i])) cards[i] = null!;
        }

        Cards.Clear();

        for (int i = cards.Length - 1; i >= 0; i--)
        {
            if (cards[i] != null) Cards.Enqueue(cards[i]);
        }
    }

    public Card NextCard() => Cards.Dequeue();

    public Card NextCard(bool open)
    {
        Card card = Cards.Dequeue();
        if (open) card.Open();
        else card.Close();
        return card;
    }

    public void Shuffle()
    {
        Card[] cards = [.. Cards];

        for (int i = cards.Length - 1; i >= 0; i--)
        {
            int j = random.Next(0, cards.Length);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }

        Cards.Clear();

        for (int i = cards.Length - 1; i >= 0; i--)
        {
            Cards.Enqueue(cards[i]);
        }
    }
}