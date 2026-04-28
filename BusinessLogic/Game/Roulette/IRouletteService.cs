namespace BusinessLogic.Game.Roulette;

public interface IRouletteService
{
    public Task<RouletteGameResult?> Spin(int userId, List<RouletteBid> bids);
}
