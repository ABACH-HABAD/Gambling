using Gambling.Application.Core.BusinessModels.GameModels.Roulette;

namespace Gambling.Application.Core.Abstractions.Game.Roulette;

public interface IRouletteWinCounterService
{
    public double WinCount(RouletteElement droppedElement, List<RouletteBid> rouletterBids);
}
