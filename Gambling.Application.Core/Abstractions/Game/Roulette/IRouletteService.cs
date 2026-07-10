using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.BusinessModels.GameModels.Roulette;

namespace Gambling.Application.Core.Abstractions.Game.Roulette;

public interface IRouletteService
{
    public Task<RouletteGameResult?> Spin(int userId, List<RouletteBid> bids);
}
