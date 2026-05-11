using System.Net.Http.Json;
using BusinessLogic.ApiServices;
using BusinessLogic.ApiServices.Requests;

namespace BusinessLogic.Game.Blackjack;

public class ClientBlackjackService(IApiClient apiClient) : ClientGameService(apiClient, GameType.Blackjack), IBlackjackService
{
    public async Task<BlackjackGameState> FirstMove(int userId, double bet)
    {
        HttpResponseMessage response;

        try
        {
            response = await _apiClient.PostAsync("/blackjack/firstMove", new BlackjackRequest(bet));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlackjackGameState>() ?? throw new Exception();
            }
            else throw new Exception();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<BlackjackGameState> TakeCard(int userId)
    {
        HttpResponseMessage response;

        try
        {
            response = await _apiClient.PostAsync("/blackjack/takeCard", null);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlackjackGameState>() ?? throw new Exception();
            }
            else throw new Exception();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<BlackjackGameState> TakeDouble(int userId)
    {
        HttpResponseMessage response;

        try
        {
            response = await _apiClient.PostAsync("/blackjack/takeDouble", null);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlackjackGameState>() ?? throw new Exception();
            }
            else throw new Exception();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<BlackjackGameState> Stand(int userId)
    {
        HttpResponseMessage response;

        try
        {
            response = await _apiClient.PostAsync("/blackjack/stand", null);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlackjackGameState>() ?? throw new Exception();
            }
            else throw new Exception();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<BlackjackGameResult> EndGame(int userId)
    {
        HttpResponseMessage response;

        try
        {
            response = await _apiClient.PostAsync("/blackjack/endGame", null);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlackjackGameResult>() ?? throw new Exception();
            }
            else throw new Exception();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int Scores(List<Card> cards)
    {
        throw new NotImplementedException();
    }
}