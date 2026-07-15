using System.Net.Http.Json;
using System.Net.Sockets;
using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Game;
using Gambling.Application.Core.Abstractions.Game.Slots;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Api.Requests;
using Gambling.Application.Core.Api.Results;
using Gambling.Core.Models;

namespace Gambling.Application.Client.Services.Game.Slots;

public class ClientSlotsService(IApiClient apiClient, IAccountDataService accountDataService) : ClientGameService(apiClient, GameType.Slots), IGameService, ISlotsService
{
    public async Task<bool> HasBonusGamesAsync(int userId)
    {
        UserModel? user = await accountDataService.GetUserDataAsync(userId);
        if (user != null) return user.SlotsBonusCount > 0;
        else return false;
    }

    public async Task<SlotGameResult?> SpinAsync(int userId, double bid, int linesCount = 3, int columnsCount = 5)
    {
        HttpResponseMessage responseMessage;

        try
        {
            responseMessage = await _apiClient.PostAsync("spinSlots", new SlotSpinRequest(userId, bid, linesCount, columnsCount));
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            return null;
        }

        SlotGameResult? gameResult = await responseMessage.Content.ReadFromJsonAsync<SlotGameResult>();
        if (gameResult != null)
        {
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK) return gameResult;
            return null;
        }
        return null;
    }
}