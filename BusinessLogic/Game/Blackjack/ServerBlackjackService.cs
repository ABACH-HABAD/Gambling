using BusinessLogic.Exceptions;
using DataBaseClasses.Entity;
using DataBaseClasses.Exceptions;
using DataBaseClasses.Repository.Interfaces;
using Windows.System;

namespace BusinessLogic.Game.Blackjack;

public class ServerBlackjackService(
    IUserRepository userRepository,
    IGameRepository gameRepository)
    : ServerGameService(userRepository, gameRepository, GameType.Blackjack), IBlackjackService, IGameService
{
    private BlackjackGame? Game { get; set; }

    private void StartGame(int userId, double bet)
    {
        Game = new BlackjackGame(userId, bet, new(), new(), new(mix: true, isOpen: false));
        Game.StartGame();
    }

    private void ContinueGame(int userId) => Game = BlackjackGame.ContinueGame(userId);

    private void AddCard(BlackjackPlayer player, bool open = true)
    {
        if (Game == null) throw new BlackjackNullGameException();
        player.AddCard(Game.CardDeck.NextCard(open));
    }

    public async Task<BlackjackGameState> FirstMove(int userId, double bet)
    {
        StartGame(userId, bet);
        if (Game == null) throw new BlackjackNullGameException();

        try
        {
            _userRepository.WriteOffFromBalance(userId, Game.Bet);
        }
        catch (InsufficientFundsException)
        {
            throw new InsufficientFundsException();
        }

        return Game.FormGameState();
    }

    public async Task<BlackjackGameState> TakeCard(int userId)
    {
        ContinueGame(userId);
        if (Game == null) throw new BlackjackNullGameException();

        AddCard(Game.Player);

        return Game.FormGameState();
    }

    public async Task<BlackjackGameState> TakeDouble(int userId)
    {
        ContinueGame(userId);
        if (Game == null) throw new BlackjackNullGameException();

        try
        {
            _userRepository.WriteOffFromBalance(userId, Game.Bet);
            AddCard(Game.Player);
            Game.DoubleBet();
        }
        catch (InsufficientFundsException)
        {
            throw new InsufficientFundsException();
        }

        return Game.FormGameState();
    }

    public async Task<BlackjackGameState> Stand(int userId)
    {
        ContinueGame(userId);
        if (Game == null) throw new BlackjackNullGameException();

        await DealerPlaying(userId);

        return Game.FormGameState();
    }

    public async Task<BlackjackGameResult> EndGame(int userId)
    {
        ContinueGame(userId);
        if (Game == null) throw new BlackjackNullGameException();

        BlackjackGameResult result = await GameResult(Game);
        _userRepository.AddToBalance(userId, result.Win);

        Game.EndGame();

        return result;
    }

    internal async Task<BlackjackGameResult> GameResult(BlackjackGame game)
    {
        int playerScores = Scores(game.Player);
        int dealerScores = Scores(game.Dealer);

        if (playerScores > 21) return new BlackjackGameResult(false, "Перебор", 0);
        else if (playerScores == 21 && dealerScores != 21) return new BlackjackGameResult(false, "Blackjack", game.Bet * 2.5);
        else if (playerScores < 21 && playerScores > dealerScores) return new BlackjackGameResult(false, "Победа", game.Bet * 2);
        else if (playerScores < 21 && playerScores == dealerScores) return new BlackjackGameResult(false, "Ничья", game.Bet);
        else return new BlackjackGameResult(false, "Проигрышь", 0);
    }

    public async Task DealerPlaying(int userId)
    {
        ContinueGame(userId);
        if (Game == null) throw new BlackjackNullGameException();

        foreach (Card card in Game.Dealer.Cards) card.Open();

        while (Scores(Game.Dealer) < 17)
        {
            await Task.Delay(1000);
            AddCard(Game.Dealer, open: true);
        }
    }

    public int Scores(BlackjackPlayer player) => Scores(player.Cards);

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
