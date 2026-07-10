using Gambling.Application.Core.Abstractions.Game.Blackjack;
using Gambling.Application.Core.BusinessModels.GameModels.Blackjack;
using Gambling.Application.Core.Services.Game.Blackjack;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Models;

namespace Gambling.Application.Core.Services.Game.Blackjack;

public class BlackjackGameService(IBlackjackGameRepository blackjackGameRepository, IBlackjackCardRepository blackjackCardRepository) : IBlackjackGameService
{
    private readonly IBlackjackGameRepository _blackjackGameRepository = blackjackGameRepository;
    private readonly IBlackjackCardRepository _blackjackCardRepository = blackjackCardRepository;

    public bool CanPlayerMove { get; private set; }
    public int PlayerId { get; private set; }
    public double Bet { get; private set; }

    public BlackjackPlayer Dealer { get; init; } = new();
    public BlackjackPlayer Player { get; init; } = new();

    private Deck? CardDeck { get; set; }

    private async Task SaveGameAsync()
    {
        if (await _blackjackGameRepository.HasSaveGameAsync(PlayerId))
            await DeleteGameAsync();

        BlackjackGameModel game = await _blackjackGameRepository.SaveGameStateAsync(PlayerId, new BlackjackGameModel() { Id = 0, UserId = PlayerId, Bet = Bet, CanPlayerMove = CanPlayerMove });
        await _blackjackCardRepository.SaveGameStateAsync(game.Id, FormGameState(allDealerCardsOpen: true).ToCardModelList());
    }

    private async Task DeleteGameAsync()
    {
        BlackjackGameModel game = await _blackjackGameRepository.GetSaveGameAsync(PlayerId);

        await _blackjackCardRepository.DeleteGameStateAsync(game.Id);
        await _blackjackGameRepository.DeleteGameStateAsync(PlayerId);
    }

    public async Task StartGameAsync(int playerId, double bet)
    {
        Player.ClearCards();
        Dealer.ClearCards();

        PlayerId = playerId;
        Bet = bet;
        CardDeck = new(mix: true, isOpen: false);

        if (!await _blackjackGameRepository.HasSaveGameAsync(PlayerId))
        {
            CanPlayerMove = true;

            Player.AddCard(CardDeck.NextCard(open: true));
            Player.AddCard(CardDeck.NextCard(open: true));

            Dealer.AddCard(CardDeck.NextCard(open: true));
            Dealer.AddCard(CardDeck.NextCard(open: false));

            await SaveGameAsync();
        }
        else throw new Exception($"Используйте {nameof(ContinueGameAsync)}");
    }

    public async Task EndGameAsync()
    {
        CanPlayerMove = false;
        if (await _blackjackGameRepository.HasSaveGameAsync(PlayerId)) await DeleteGameAsync();
    }

    public async Task AddNextCardToPlayerAsync(BlackjackPlayer player, bool open = true)
    {
        if (CardDeck == null) throw new Exception($"{nameof(CardDeck)} не задан. Используйте {nameof(StartGameAsync)} или {nameof(ContinueGameAsync)}");
        player.AddCard(CardDeck.NextCard(open: open));

        await SaveGameAsync();
    }

    public async Task PlayerEndMoveAsync()
    {
        CanPlayerMove = false;
        await SaveGameAsync();
    }

    public async Task AddToBetAsync(double bet)
    {
        Bet += bet;
        await SaveGameAsync();
    }

    public async Task DoubleBetAsync()
    {
        Bet *= 2;
        await SaveGameAsync();
    }

    public async Task ContinueGameAsync(int playerId)
    {
        Player.ClearCards();
        Dealer.ClearCards();

        CardDeck ??= new(mix: true, isOpen: false);

        BlackjackGameModel game = await _blackjackGameRepository.GetSaveGameAsync(playerId);

        Bet = game.Bet;
        PlayerId = game.UserId;
        CanPlayerMove = game.CanPlayerMove;

        BlackjackGameState state = (await _blackjackCardRepository.GetSaveGameAsync(game.Id)).ToBlackjackGameState();

        foreach (Card playerCard in state.PlayerCards)
        {
            Player.AddCard(playerCard);
            CardDeck.RemoveCards(card => card.Suit == playerCard.Suit && card.Denomination == playerCard.Denomination);
        }

        foreach (Card dealerCard in state.DealerCards)
        {
            Dealer.AddCard(dealerCard);
            CardDeck.RemoveCards(card => card.Suit == dealerCard.Suit && card.Denomination == dealerCard.Denomination);
        }

        await SaveGameAsync();
    }

    public BlackjackGameState FormGameState(bool allDealerCardsOpen = false)
    {
        if (Dealer == null || Player == null) throw new Exception($"{nameof(Dealer)} или {nameof(Player)} не заданы");
        if (allDealerCardsOpen || Dealer.Cards.Count > 2) return new BlackjackGameState(IsOk: true, PlayerCards: Player.Cards, DealerCards: Dealer.Cards, Bet: Bet);
        else return new BlackjackGameState(IsOk: true, PlayerCards: Player.Cards, DealerCards: [Dealer.Cards[0], Card.UnkownCard()], Bet: Bet);
    }
}