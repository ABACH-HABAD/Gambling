using Gambling.Application.Core.Abstractions.Game.Blackjack;
using Gambling.Application.Core.BusinessModels.GameModels.Blackjack;

namespace Gambling.Application.Core.Services.Game.Blackjack;

public class BlackjackScoresService : IBlackjackScoresService
{
    public int Scores(BlackjackPlayer player, bool onlyOpenCards = false) => Scores(player.Cards, onlyOpenCards);

    public int Scores(List<Card> cards, bool onlyOpenCards = false)
    {
        int scores = 0;
        int aces = 0;
        foreach (Card card in cards)
        {
            if (card.Denomination == "?") card.Close();
            if (onlyOpenCards && !card.IsOpen) continue;

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