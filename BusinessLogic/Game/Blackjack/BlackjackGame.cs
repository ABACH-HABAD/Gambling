namespace BusinessLogic.Game.Blackjack;

internal class BlackjackGame
{
    internal static List<BlackjackGame> ActiveGames { get; } = [];

    internal int PlayerId { get; init; }
    internal double Bet { get; private set; }

    internal BlackjackPlayer Dealer { get; init; }
    internal BlackjackPlayer Player { get; init; }

    internal Deck CardDeck { get; init; }

    internal BlackjackGame(int playerId, double bet, BlackjackPlayer dealer, BlackjackPlayer player, Deck cardDeck)
    {
        PlayerId = playerId;
        Bet = bet;
        Dealer = dealer;
        Player = player;
        CardDeck = cardDeck;
    }

    internal void StartGame()
    {
        if (!ActiveGames.Contains(this))
        {
            ActiveGames.Add(this);

            Player.AddCard(CardDeck.NextCard(open: true));
            Player.AddCard(CardDeck.NextCard(open: true));

            Player.AddCard(CardDeck.NextCard(open: true));
            Dealer.AddCard(CardDeck.NextCard(open: false));
        }
    }

    internal void EndGame()
    {
        if (ActiveGames.Contains(this)) ActiveGames.Remove(this);
    }

    internal void DoubleBet() => Bet *= 2;

    internal static BlackjackGame? ContinueGame(int playerId)
    {
        return ActiveGames.FirstOrDefault(game => game.PlayerId == playerId);
    }

    internal BlackjackGameState FormGameState()
    {
        return new BlackjackGameState(PlayerCards: Player.Cards, DealerCards: Dealer.Cards);
    }
}