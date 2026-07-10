using System.Net.Http.Json;
using Gambling.Application.Client.Services.ApiServices;
using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Game.Blackjack;
using Gambling.Application.Core.Api.Requests;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.BusinessModels.GameModels.Blackjack;

namespace Gambling.Application.Client.Services.Game.Blackjack;

public class ClientBlackjackService(IApiClient apiClient) : ClientGameService(apiClient, GameType.Blackjack), IBlackjackService
{
    public async Task<BlackjackGameState> TryContinueGameAsync(int userId = 0)
    {
        HttpResponseMessage response;

        try
        {
            response = await _apiClient.GetAsync("/blackjack/tryContinueGame");
            if (response.IsSuccessStatusCode)
            {
                return await response.WithTypeAsync<BlackjackGameState>() ?? throw new Exception();
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new BlackjackGameState(false, null!, null!, Message: $"Во время игры мы зафиксировали попытку вмешательства в игровой процесс!\nДля честности игра была отменена\nВсе средства отправлены на счёт казино");
            }
            else throw new Exception(response.StatusCode.ToString());
        }
        catch (Exception ex)
        {
            return new BlackjackGameState(false, null!, null!, Message: ex.Message);
        }
    }

    public async Task<BlackjackGameState> FirstMoveAsync(int userId, double bet)
    {
        HttpResponseMessage response;

        try
        {
            response = await _apiClient.PostAsync("/blackjack/firstMove", new BlackjackRequest(userId, bet));
            if (response.IsSuccessStatusCode)
            {
                return await response.WithTypeAsync<BlackjackGameState>() ?? throw new Exception();
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new BlackjackGameState(false, null!, null!, Message: $"Во время игры мы зафиксировали попытку вмешательства в игровой процесс!\nДля честности игра была отменена\nВсе средства отправлены на счёт казино");
            }
            else throw new Exception(response.StatusCode.ToString());
        }
        catch (Exception ex)
        {
            return new BlackjackGameState(false, null!, null!, Message: ex.Message);
        }
    }

    public async Task<BlackjackGameState> TakeCardAsync(int userId)
    {
        HttpResponseMessage response;

        try
        {
            response = await _apiClient.PostAsync("/blackjack/takeCard", null);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlackjackGameState>() ?? throw new Exception();
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new BlackjackGameState(false, null!, null!, Message: $"Во время игры мы зафиксировали попытку вмешательства в игровой процесс!\nДля честности игра была отменена\nВсе средства отправлены на счёт казино");
            }
            else throw new Exception(response.StatusCode.ToString());
        }
        catch (Exception ex)
        {
            return new BlackjackGameState(false, null!, null!, Message: ex.Message);
        }
    }

    public async Task<BlackjackGameState> TakeDoubleAsync(int userId)
    {
        HttpResponseMessage response;

        try
        {
            response = await _apiClient.PostAsync("/blackjack/takeDouble", null);
            if (response.IsSuccessStatusCode)
            {
                return await response.WithTypeAsync<BlackjackGameState>() ?? throw new Exception();
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new BlackjackGameState(false, null!, null!, Message: $"Во время игры мы зафиксировали попытку вмешательства в игровой процесс!\nДля честности игра была отменена\nВсе средства отправлены на счёт казино");
            }
            else throw new Exception(response.StatusCode.ToString());
        }
        catch (Exception ex)
        {
            return new BlackjackGameState(false, null!, null!, Message: ex.Message);
        }
    }

    public async Task<BlackjackGameState> StandAsync(int userId)
    {
        HttpResponseMessage response;

        try
        {
            response = await _apiClient.PostAsync("/blackjack/stand", null);
            if (response.IsSuccessStatusCode)
            {
                BlackjackGameState gamestate = await response.WithTypeAsync<BlackjackGameState>() ?? throw new Exception();
                return gamestate;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new BlackjackGameState(false, null!, null!, Message: $"Во время игры мы зафиксировали попытку вмешательства в игровой процесс!\nДля честности игра была отменена\nВсе средства отправлены на счёт казино");
            }
            else throw new Exception(response.StatusCode.ToString());
        }
        catch (Exception ex)
        {
            return new BlackjackGameState(false, null!, null!, Message: ex.Message);
        }
    }

    public async Task<BlackjackGameResult> EndGameAsync(int userId)
    {
        HttpResponseMessage response;

        try
        {
            response = await _apiClient.PostAsync("/blackjack/endGame", null);
            if (response.IsSuccessStatusCode)
            {
                return await response.WithTypeAsync<BlackjackGameResult>() ?? throw new Exception();
            }
            else throw new Exception(response.StatusCode.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}