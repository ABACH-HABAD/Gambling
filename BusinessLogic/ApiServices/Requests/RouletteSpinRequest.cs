using BusinessLogic.Game.Roulette;

namespace BusinessLogic.ApiServices.Requests;

public record RouletteSpinRequest(List<RouletteBidJson> Bids) : BaseRequest();