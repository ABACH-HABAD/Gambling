namespace BusinessLogic.Game.Roulette;

public interface IRouletteWinCounterService
{
    public double WinCount(RouletteElement droppedElement, List<RouletteBid> rouletterBids);
}
