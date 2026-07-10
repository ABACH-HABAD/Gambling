using System.Net.Http.Json;
using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Game;
using Gambling.Core.Models;

namespace Gambling.Application.Client.Services.Game;

public class ClientGameService(IApiClient apiClient, GameType defaultGameType = GameType.Any) : IGameService
{
    protected readonly IApiClient _apiClient = apiClient;
    protected GameType DefaultGameType { get; init; } = defaultGameType;

    public async Task<List<GameModel>> GetAllGamesAsync(GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await _apiClient.GetAsync($"/getGames?gameType={(int)type}");
            if (response.IsSuccessStatusCode)
            {
                List<GameModel>? games = await response.Content.ReadFromJsonAsync<List<GameModel>>();
                return games ?? [];
            }
            else return [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<List<GameModel>> GetAllGamesAsync() => await GetAllGamesAsync( DefaultGameType);
}