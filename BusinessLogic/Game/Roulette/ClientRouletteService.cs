using BusinessLogic.ApiServices;
using BusinessLogic.ApiServices.Requests;
using System.Net.Http.Json;
using System.Net.Sockets;

namespace BusinessLogic.Game.Roulette;

public class ClientRouletteService(IApiClient apiClient) : ClientGameService(apiClient, GameType.Roulette), IRouletteService, IGameService
{
    public async Task<RouletteGameResult?> Spin(int userId, List<RouletteBid> bets)
    {
        HttpResponseMessage responseMessage;

        try
        {
            responseMessage = await _apiClient.PostAsync("spinRoulette", new RouletteSpinRequest(bets.PrepareToJson()));
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            return null;
        }

        RouletteGameResult? gameResult = await responseMessage.Content.ReadFromJsonAsync<RouletteGameResult>();
        if (gameResult != null)
        {
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK) return gameResult;
            return null;
        }
        return null;
    }
}
