using BusinessLogic.ApiServices;
using System.Net.Http.Json;

namespace BusinessLogic.Game
{
    public class ClientGameService(IApiClient apiClient, GameType defaultGameType = GameType.Any) : IGameService
    {
        protected readonly IApiClient _apiClient = apiClient;
        protected GameType DefaultGameType { get; init; } = defaultGameType;

        public async Task<List<DataBaseClasses.Entity.Game>> GetAllGamesAsync(int adminId, GameType type)
        {
            HttpResponseMessage response;
            try
            {
                response = await _apiClient.GetAsync($"/getGames?gameType={(int)type}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<DataBaseClasses.Entity.Game>>() ?? [];
                }
                else return [];
            }
            catch
            {
                return [];
            }
        }

        public async Task<List<DataBaseClasses.Entity.Game>> GetAllGamesAsync(int adminId) => await GetAllGamesAsync(adminId, DefaultGameType);
    }
}
