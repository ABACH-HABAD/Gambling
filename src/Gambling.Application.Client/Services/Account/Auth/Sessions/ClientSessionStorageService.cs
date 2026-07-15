using Gambling.Application.Client.Services.ApiServices;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Sessions;
using Gambling.Core.Models;

namespace Gambling.Application.Client.Services.Account.Auth.Sessions;

public class ClientSessionStorageService(IApiClient apiClient) : ISessionStorageService
{
    public async Task<List<SessionModel>> GetAllSessionsAsync()
    {
        HttpResponseMessage response;

        try
        {
            response = await apiClient.GetAsync("getAllSessions");

            if (response.IsSuccessStatusCode)
            {
                return await response.WithTypeAsync<List<SessionModel>>() ?? [];
            }
            else throw new Exception(response.StatusCode.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}