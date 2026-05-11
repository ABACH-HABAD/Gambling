using BusinessLogic.Account;
using BusinessLogic.ApiServices;
using BusinessLogic.ApiServices.Requests;
using DataBaseClasses.Entity;
using System.Net.Http.Json;
using System.Net.Sockets;

namespace BusinessLogic.Game.Slots;

public class ClientSlotsService(IApiClient apiClient, IAccountDataService accountDataService) : ClientGameService(apiClient, GameType.Slots), IGameService, ISlotsService
{
    public async Task<bool> HasBonusGames(int userId)
    {
        User? user = await accountDataService.GetUserDataAsync(userId);
        if (user != null) return user.SlotsBonusCount > 0;
        else return false;
    }

    public async Task<SlotGameResult?> Spin(int userId, double bid, int linesCount = 3, int columnsCount = 5)
    {
        HttpResponseMessage responseMessage;

        try
        {
            responseMessage = await _apiClient.PostAsync("spinSlots", new SlotSpinRequest(bid, linesCount, columnsCount));
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
