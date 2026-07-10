using System.Net.Http.Json;
using System.Net.Sockets;
using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Game;
using Gambling.Application.Core.Abstractions.Game.Roulette;
using Gambling.Application.Core.Api.Requests;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.BusinessModels.GameModels.Roulette;
using Gambling.Application.Core.Services.Game.Roulette;

namespace Gambling.Application.Client.Services.Game.Roulette;

public class ClientRouletteService(IApiClient apiClient) : ClientGameService(apiClient, GameType.Roulette), IRouletteService, IGameService
{
    public async Task<RouletteGameResult?> Spin(int userId, List<RouletteBid> bets)
    {
        HttpResponseMessage responseMessage;

        try
        {
            responseMessage = await _apiClient.PostAsync("spinRoulette", new RouletteSpinRequest(userId, bets.PrepareToJson()));
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