using System.Net.Http.Json;
using System.Net.Sockets;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Api.Requests;
using Gambling.Application.Core.Api.Results;

namespace Gambling.Application.Client.Services.Account.Auth;

public class ClientLoginChecker(IApiClient apiClient) : ILoginChecker
{
    public async Task<LoginResult> CheckActiveLoginAsync(string refreshToken, int deviceType, string? ip)
    {
        HttpResponseMessage response;
        try
        {
            response = await apiClient.PostAsync("autoLogin", new RefreshTokenRequest(refreshToken, deviceType));
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            return new LoginResult(null, false, "Ошибка сервера");
        }

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            LoginResult? loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
            if (loginResult != null)
            {
                await apiClient.SetTokenAsync(loginResult.Tokens);
                return loginResult;
            }
            else return new LoginResult(null, false, "Аккаунт не найден");
        }
        else return new LoginResult(null, false, "Аккаунт не найден");
    }
}