using Gambling.Application.Core.BusinessModels.GameModels.Roulette;

namespace Gambling.Application.Core.Api.Requests;

public record RouletteSpinRequest(int UserId, List<RouletteBidJson> Bids) : BaseRequiringIdRequest(UserId);