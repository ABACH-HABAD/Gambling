using BusinessLogic.ApiServices;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Token;
using System.Net.Http.Json;
using System.Net.Sockets;

namespace BusinessLogic.Auth;

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
