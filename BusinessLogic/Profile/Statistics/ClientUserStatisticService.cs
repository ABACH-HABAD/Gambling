using BusinessLogic.ApiServices;
using DataBaseClasses.Entity;
using System.Net.Http.Json;

namespace BusinessLogic.Profile.Statistics;

public class ClientUserStatisticService(IApiClient apiClient) : IUserStatisticsService
{
    public async Task<double> WinFrequency(int userId, Game.GameType type)
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

    public async Task<int> WinCount(int userId, Game.GameType type)
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

    public async Task<int> LossCount(int userId, Game.GameType type)
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
    public async Task<int> TotalCount(int userId, Game.GameType type)
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
    public async Task<double> WinBalance(int userId, Game.GameType type)
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
    public async Task<double> LossBalance(int userId, Game.GameType type)
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
    public async Task<double> TotalBalance(int userId, Game.GameType type)
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

    public async Task<double> WinFrequency(User user, Game.GameType type) => await WinFrequency(user == null ? 0 : user.Id, type);
    public async Task<int> WinCount(User user, Game.GameType type) => await WinCount(user == null ? 0 : user.Id, type);
    public async Task<int> LossCount(User user, Game.GameType type) => await LossCount(user == null ? 0 : user.Id, type);
    public async Task<int> TotalCount(User user, Game.GameType type) => await TotalCount(user == null ? 0 : user.Id, type);
    public async Task<double> WinBalance(User user, Game.GameType type) => await WinBalance(user == null ? 0 : user.Id, type);
    public async Task<double> LossBalance(User user, Game.GameType type) => await LossBalance(user == null ? 0 : user.Id, type);
    public async Task<double> TotalBalance(User user, Game.GameType type) => await TotalBalance(user == null ? 0 : user.Id, type);
}