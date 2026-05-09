using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Net.Sockets;
using DataBaseClasses.Entity;
using BusinessLogic.ApiServices;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Encryption;
using BusinessLogic.Token;
using BusinessLogic.Validation;

namespace BusinessLogic.Auth;

public class ClientAccountService(
    IApiClient apiClient, 
    IValidation emailValidation, 
    ITwoPasswordsValidation passwordValidation, 
    IPasswordHasher passwordHasher,
    [FromKeyedServices("refresh")] ITokenStorageService tokenStorage)
    : IAccountService
{
    public async Task<LoginResult> RegistrateAsync(string login, string rawPassword, string repeatPassword, DeviceType deviceType, string? ip)
    {
        if (emailValidation is not EmailValidation) throw new Exception("Валидация почты не настроена");
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
            response = await apiClient.PostAsync("login", new LoginRequest(login, passwordHasher.HashPassword(rawPassword), deviceTypeId, loginAsAdmin));
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
        if (refreshToken == null || refreshToken == string.Empty) refreshToken = await tokenStorage.GetTokenAsync() ?? throw new Exception("Сессия не найдена");

        //запрос
        HttpResponseMessage response;
        try
        {
            response = await apiClient.PostAsync("autoLogin", new RefreshTokenRequest(refreshToken, (int)deviceType));
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

    public async Task LogoutAsync(string refreshToken, DeviceType deviceType, string? ip = null)
    {
        if (refreshToken == null || refreshToken == string.Empty) refreshToken = await tokenStorage.GetTokenAsync() ?? throw new Exception("Сессия не найдена");

        await apiClient.PostAsync("logout", new RefreshTokenRequest(refreshToken, (int)deviceType));

        await tokenStorage.SaveTokenAsync(null!);
        await apiClient.SetTokenAsync(null!);
    }

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
