namespace Gambling.Application.Core.BusinessModels.GameModels.Blackjack;

public class BlackjackPlayer(List<Card>? cards = null)
{
    public List<Card> Cards { get; init; } = cards ?? [];

    public void AddCard(Card card) => Cards.Add(card);
    public void ClearCards() => Cards.Clear();
}