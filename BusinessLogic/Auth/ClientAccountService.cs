using BusinessLogic.ApiServices;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Auth.Validation;
using BusinessLogic.Encryption;
using DataBaseClasses.Entity;
using System.Net.Http.Json;
using System.Net.Sockets;

namespace BusinessLogic.Auth;

public class ClientAccountService(IApiClient apiClient, IValidation emailValidation, ITwoPasswordsValidation passwordValidation, IPasswordHasher passwordHasher) : IAccountService
{
    public async Task<LoginResult> RegistrateAsync(string login, string rawPassword, string repeatPassword, DeviceType deviceType, string? ip)
    {
        //валидация
        if (!emailValidation.Validate(login, out string loginError)) return new LoginResult(null, false, loginError);
        if (!passwordValidation.ValidateTwoPasswords(rawPassword, repeatPassword, out string passwordError)) return new LoginResult(null, false, passwordError);
        int deviceTypeId = (int)deviceType;

        //запрос
        HttpResponseMessage response;
        try
        {
            response = await apiClient.PostAsync("registrate", new RegistrateRequest(login, passwordHasher.HashPassword(rawPassword), passwordHasher.HashPassword(repeatPassword), deviceTypeId));
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            return new LoginResult(null, false, "Сервер не отвечает");
        }

        //ответ
        LoginResult? result = await response.Content.ReadFromJsonAsync<LoginResult>();
        if (result != null)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (result.Tokens == null) throw new Exception("Токены отсутсвуют");
                await apiClient.SetTokenAsync(result.Tokens);
                return result;
            }
            else return result;
        }
        else return new LoginResult(null, false, "Ошибка при чтении ответа с сервера");
    }

    public async Task<LoginResult> LoginAsync(string login, string rawPassword, DeviceType deviceType, string? ip = null, bool loginAsAdmin = false)
    {
        //валидация
        if (!emailValidation.Validate(login, out string loginError)) return new LoginResult(null, false, loginError);
        int deviceTypeId = (int)deviceType;

        //запрос
        HttpResponseMessage response;
        try
        {
            response = await apiClient.PostAsync("login", new LoginRequest(login, passwordHasher.HashPassword(rawPassword), deviceTypeId));
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            return new LoginResult(null, false, "Сервер не отвечает");
        }

        //ответ
        LoginResult? result = await response.Content.ReadFromJsonAsync<LoginResult>();
        if (result != null)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (result.Tokens == null) throw new Exception("Token is null");
                await apiClient.SetTokenAsync(result.Tokens);
                return result;
            }
            else return result;
        }
        else return new LoginResult(null, false, "Ошибка при чтении ответа с сервера");
    }

    public async Task<LoginResult> AutoLoginAsync(string refreshToken, DeviceType deviceType, string? ip = null)
    {
        //ц
        HttpResponseMessage response;
        try
        {
            response = await apiClient.PostAsync("login", new RefreshTokenRequest(refreshToken, (int)deviceType));
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            return new LoginResult(null, false, "Сервер не отвечает");
        }

        //ответ
        LoginResult? result = await response.Content.ReadFromJsonAsync<LoginResult>();
        if (result != null)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (result.Tokens == null) throw new Exception("Token is null");
                await apiClient.SetTokenAsync(result.Tokens);
                return result;
            }
            else return result;
        }
        else return new LoginResult(null, false, "Ошибка при чтении ответа с сервера");
    }

    public async Task<bool> CheckRegistrationAsync(string login)
    {
        //запрос
        HttpResponseMessage response;
        try
        {
            response = await apiClient.PostAsync("checkRegistration", login);
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            return false;
        }

        //ответ
        LoginResult? result = await response.Content.ReadFromJsonAsync<LoginResult>();
        if (result != null) return result.Result;
        else return false;
    }

    public async Task<User?> GetUserData(int userId)
    {
        HttpResponseMessage message;
        try
        {
            message = await apiClient.GetAsync($"getUserData?userId={userId}");

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await message.Content.ReadFromJsonAsync<User>();
            }
            else return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<User?> UpdateUserDataAsync(User user)
    {
        HttpResponseMessage message;
        try
        {
            message = await apiClient.PutAsync("putUserData", user);

            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await message.Content.ReadFromJsonAsync<User>();
            }
            else return null;
        }
        catch
        {
            return null;
        }
    }

    public Task<bool> TopUpBalance(int userId, double sum)
    {
        throw new NotImplementedException();
    }
}
