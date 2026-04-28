using BusinessLogic.ApiServices;
using System.Net.Http.Json;
using System.Net.Sockets;

namespace BusinessLogic.Game.Roulette;

public class ClientRouletteService(IApiClient apiClient) : IRouletteService, IGameService
{
    public async Task<RouletteGameResult?> Spin(int userId, List<RouletteBid> bids)
    {
        HttpResponseMessage responseMessage;

        try
        {
            responseMessage = await apiClient.PostAsync("spinRoulette", new { bids });
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
