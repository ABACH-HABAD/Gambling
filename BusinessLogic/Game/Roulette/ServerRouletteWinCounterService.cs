namespace BusinessLogic.Game.Roulette;

public class ServerRouletteWinCounterService : IRouletteWinCounterService
{
    public double WinCount(RouletteElement droppedElement, List<RouletteBid> rouletterBids)
    {
        double win = 0;

        foreach (RouletteBid bid in rouletterBids)
        {
            bool bidWorked = false;

            if (int.TryParse(bid.Type, out int result)) bidWorked = droppedElement.Value == result;

            else if (bid.Type == "First12") bidWorked = droppedElement.IsFirst12;
            else if (bid.Type == "Second12") bidWorked = droppedElement.IsSecond12;
            else if (bid.Type == "Third12") bidWorked = droppedElement.IsThird12;
            else if (bid.Type == "FirstColumn") bidWorked = droppedElement.IsFirstColumn;
            else if (bid.Type == "SecondColumn") bidWorked = droppedElement.IsSecondColumn;
            else if (bid.Type == "ThirdColumn") bidWorked = droppedElement.IsThirdColumn;
            else if (bid.Type == "FirstHalf") bidWorked = droppedElement.IsFirstHalf;
            else if (bid.Type == "SecondHalf") bidWorked = droppedElement.IsSecondHalf;
            else if (bid.Type == "Even") bidWorked = droppedElement.IsEven;
            else if (bid.Type == "Odd") bidWorked = droppedElement.IsOdd;
            else if (bid.Type == "Red") bidWorked = droppedElement.IsRed;
            else if (bid.Type == "Black") bidWorked = droppedElement.IsBlack;

            if (bidWorked) win += bid.Coefficent * bid.BidCount;
        }

        return win;
    }
}
