using System.Net.Http.Json;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Api.Requests;
using Gambling.Core.Models;

namespace Gambling.Application.Client.Services.Account;

public class ClientAccountDataService(IApiClient apiClient) : IAccountDataService
{
    public async Task<UserModel?> GetUserDataAsync(int userId)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userData?userId={userId}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<UserModel>();
            }
            else return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<UserModel>> GetAllUsersAsync()
    {
        //запрос
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync("getAllUsers");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<UserModel>? users = await response.Content.ReadFromJsonAsync<List<UserModel>>();
                return users ?? [];
            }
            else return [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<bool> ChangeNameAsync(int userId, string name)
    {
        HttpResponseMessage responce;
        try
        {
            responce = await apiClient.PutAsync("changeName", new ChangeNameRequest(userId, name));

            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<bool>();
            }
            else return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ChangeBalanceAsync(int userId, double sum)
    {
        HttpResponseMessage responce;
        try
        {
            responce = await apiClient.PutAsync("changeBalance", new ChangeBalanceRequest(userId, sum));

            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<bool>();
            }
            else return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ChangeStatusAsync(int userId, int statusId)
    {
        HttpResponseMessage responce;
        try
        {
            responce = await apiClient.PutAsync("changeStatus", new ChangeStatusRequest(userId, statusId));

            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<bool>();
            }
            else return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> BlockUser(int userId)
    {
        HttpResponseMessage responce;
        try
        {
            responce = await apiClient.PutAsync("block", new BlockRequest(userId));

            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<bool>();
            }
            else return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UnblockUser(int userId)
    {
        HttpResponseMessage responce;
        try
        {
            responce = await apiClient.PutAsync("unblock", new UnblockRequest(userId));

            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<bool>();
            }
            else return false;
        }
        catch
        {
            return false;
        }
    }
}