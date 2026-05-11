using BusinessLogic.ApiServices;
using DataBaseClasses.Entity;
using System.Net.Http.Json;

namespace BusinessLogic.Account;

public class ClientAccountDataService(IApiClient apiClient) : IAccountDataService
{
    public async Task<User?> GetUserDataAsync(int userId)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync($"userData");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            else return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<User>> GetAllUsersAsync(int adminId)
    {
        //запрос
        HttpResponseMessage response;
        try
        {
            response = await apiClient.GetAsync("getAllUsers");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<User>>() ?? [];
            }
            else return [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<User?> UpdateUserDataAsync(User user)
    {
        HttpResponseMessage responce;
        try
        {
            responce = await apiClient.PutAsync("userData", user);

            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<User>();
            }
            else return null;
        }
        catch
        {
            return null;
        }
    }

    public Task<bool> ChangeBalanceAsync(int userId, double sum)
    {
        throw new NotImplementedException();
    }
}
