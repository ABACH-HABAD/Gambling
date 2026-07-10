using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Statistics;
using Gambling.Application.Core.Api.Results;
using Gambling.Core.Models;
using System.Net.Http.Json;

namespace Gambling.Application.Client.Services.Account.Profile.Statistics;

public class ClientUserStatisticService(IApiClient apiClient) : IUserStatisticsService
{
    public async Task<UserStatisticResult> GetUserStatisticResultAsync(int userId, GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic?gameType={(int)type}&userId={userId}");

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

    public async Task<double> GetWinFrequencyAsync(int userId, GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/frequency?gameType={(int)type}&userId={userId}");

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

    public async Task<int> GetWinCountAsync(int userId, GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/count/win?gameType={(int)type}&userId={userId}");

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

    public async Task<int> GetLossCountAsync(int userId, GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/count/loss?gameType={(int)type}&userId={userId}");

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
    public async Task<int> GetTotalCountAsync(int userId, GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/count/total?gameType={(int)type}&userId={userId}");

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
    public async Task<double> GetWinBalanceAsync(int userId, GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/balance/win?gameType={(int)type}&userId={userId}");

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
    public async Task<double> GetLossBalanceAsync(int userId, GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/balance/loss?gameType={(int)type}&userId={userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<double>();
            }
            else throw new Exception($"{response.StatusCode} {response.RequestMessage}" + (userId != 0 ? $"&userId={userId}" : string.Empty));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<double> GetTotalBalanceAsync(int userId, GameType type)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userStatistic/balance/total?gameType={(int)type}&userId={userId}");

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

    public async Task<UserStatisticResult> GetUserStatisticResultAsync(UserModel user, GameType type) => await GetUserStatisticResultAsync(user == null ? 0 : user.Id, type);

    public async Task<double> GetWinFrequencyAsync(UserModel user, GameType type) => await GetWinFrequencyAsync(user == null ? 0 : user.Id, type);
    public async Task<int> GetWinCountAsync(UserModel user, GameType type) => await GetWinCountAsync(user == null ? 0 : user.Id, type);
    public async Task<int> GetLossCountAsync(UserModel user, GameType type) => await GetLossCountAsync(user == null ? 0 : user.Id, type);
    public async Task<int> GetTotalCountAsync(UserModel user, GameType type) => await GetTotalCountAsync(user == null ? 0 : user.Id, type);
    public async Task<double> GetWinBalanceAsync(UserModel user, GameType type) => await GetWinBalanceAsync(user == null ? 0 : user.Id, type);
    public async Task<double> GetLossBalanceAsync(UserModel user, GameType type) => await GetLossBalanceAsync(user == null ? 0 : user.Id, type);
    public async Task<double> GetTotalBalanceAsync(UserModel user, GameType type) => await GetTotalBalanceAsync(user == null ? 0 : user.Id, type);
}