using BusinessLogic.ApiServices;
using DataBaseClasses.Entity;
using System.Net.Http.Json;

namespace BusinessLogic.Account.Profile.Statistics;

public class ClientUserStatisticService(IApiClient apiClient) : IUserStatisticsService
{
    public async Task<UserStatisticResult> GetUserStatisticResultAsync(int userId, Game.GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic?gameType={(int)type}"); //

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserStatisticResult>() ?? throw new Exception("Не удалось загрузить статистику");
            }
            else throw new Exception($"{response.StatusCode} {response.RequestMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<double> GetWinFrequencyAsync(int userId, Game.GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/frequency?gameType={(int)type}"); //

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<double>();
            }
            else throw new Exception($"{response.StatusCode} {response.RequestMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<int> GetWinCountAsync(int userId, Game.GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/count/win?gameType={(int)type}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }
            else throw new Exception($"{response.StatusCode} {response.RequestMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<int> GetLossCountAsync(int userId, Game.GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/count/loss?gameType={(int)type}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }
            else throw new Exception($"{response.StatusCode} {response.RequestMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<int> GetTotalCountAsync(int userId, Game.GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/count/total?gameType={(int)type}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }
            else throw new Exception($"{response.StatusCode} {response.RequestMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<double> GetWinBalanceAsync(int userId, Game.GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/balance/win?gameType={(int)type}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<double>();
            }
            else throw new Exception($"{response.StatusCode} {response.RequestMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<double> GetLossBalanceAsync(int userId, Game.GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/balance/loss?gameType={(int)type}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<double>();
            }
            else throw new Exception($"{response.StatusCode} {response.RequestMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<double> GetTotalBalanceAsync(int userId, Game.GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/balance/total?gameType={(int)type}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<double>();
            }
            else throw new Exception($"{response.StatusCode} {response.RequestMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserStatisticResult> GetUserStatisticResultAsync(User user, Game.GameType type) => await GetUserStatisticResultAsync(user == null ? 0 : user.Id, type);

    public async Task<double> GetWinFrequencyAsync(User user, Game.GameType type) => await GetWinFrequencyAsync(user == null ? 0 : user.Id, type);
    public async Task<int> GetWinCountAsync(User user, Game.GameType type) => await GetWinCountAsync(user == null ? 0 : user.Id, type);
    public async Task<int> GetLossCountAsync(User user, Game.GameType type) => await GetLossCountAsync(user == null ? 0 : user.Id, type);
    public async Task<int> GetTotalCountAsync(User user, Game.GameType type) => await GetTotalCountAsync(user == null ? 0 : user.Id, type);
    public async Task<double> GetWinBalanceAsync(User user, Game.GameType type) => await GetWinBalanceAsync(user == null ? 0 : user.Id, type);
    public async Task<double> GetLossBalanceAsync(User user, Game.GameType type) => await GetLossBalanceAsync(user == null ? 0 : user.Id, type);
    public async Task<double> GetTotalBalanceAsync(User user, Game.GameType type) => await GetTotalBalanceAsync(user == null ? 0 : user.Id, type);
}