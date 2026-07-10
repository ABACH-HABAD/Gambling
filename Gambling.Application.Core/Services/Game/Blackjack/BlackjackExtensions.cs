using Gambling.Application.Core.BusinessModels.GameModels.Blackjack;
using Gambling.Core.Models;

namespace Gambling.Application.Core.Services.Game.Blackjack;

public static class BlackjackExtensions
{
    public static Card ToCard(this BlackjackCardModel databaseCard)
    {
        return new Card(denomination: databaseCard.Denomination ?? "error", suit: databaseCard.Suit, isOpen: databaseCard.IsOpen);
    }

    public static BlackjackGameState ToBlackjackGameState(this List<BlackjackCardModel> databaseCardList)
    {
        List<Card> PlayerCards = [];
        List<Card> DealerCards = [];

        try
        {
            foreach (BlackjackCardModel card in databaseCardList)
            {
                if (card.InPlayerHand) PlayerCards.Add(card.ToCard());
                else DealerCards.Add(card.ToCard());
            }

            return new BlackjackGameState(IsOk: true, PlayerCards: PlayerCards, DealerCards: DealerCards);
        }
        catch (Exception ex)
        {
            return new BlackjackGameState(IsOk: false, null!, null!, Message: ex.Message);
        }
    }

    public static List<BlackjackCardModel> ToCardModelList(this BlackjackGameState gamestate)
    {
        List<BlackjackCardModel> cardlist = [];
        foreach (Card card in gamestate.PlayerCards) cardlist.Add(new BlackjackCardModel() { Id = 0, Denomination = card.Denomination, Suit = card.Suit, IsOpen = card.IsOpen, InPlayerHand = true });
        foreach (Card card in gamestate.DealerCards) cardlist.Add(new BlackjackCardModel() { Id = 0, Denomination = card.Denomination, Suit = card.Suit, IsOpen = card.IsOpen, InPlayerHand = false });
        return cardlist;
    }
}