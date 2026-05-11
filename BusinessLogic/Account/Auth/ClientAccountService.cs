using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Net.Sockets;
using BusinessLogic.ApiServices;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Encryption;
using BusinessLogic.Token;
using BusinessLogic.Validation;
using BusinessLogic.Exceptions;

namespace BusinessLogic.Account.Auth;

public class ClientAccountService(
    IApiClient apiClient,
    IEmailValidation emailValidation,
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
        //валидация
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

    public async Task<bool> ChangeEmailAsync(int userId, string oldEmail, string newEmail)
    {
        //валидация
        if (!emailValidation.Validate(oldEmail, out string error1)) throw new ValidationException(error1);
        if (!emailValidation.Validate(newEmail, out string error2)) throw new ValidationException(error2);

        //запрос
        HttpResponseMessage response;
        try
        {
            response = await apiClient.PutAsync("changeEmail", new ChangeEmailRequest(oldEmail, newEmail));
        }
        catch
        {
            return false;
        }

        //ответ
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        else return false;
    }

    public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword, string repeatPassword)
    {
        //валидация
        if (!passwordValidation.Validate(oldPassword, out string error1)) throw new ValidationException(error1);
        if (!passwordValidation.ValidateTwoPasswords(newPassword, repeatPassword, out string error2)) throw new ValidationException(error2);
        string oldHashedPassword = passwordHasher.HashPassword(oldPassword);
        string newHashedPassword = passwordHasher.HashPassword(newPassword);
        string repeatHashedPassword = passwordHasher.HashPassword(repeatPassword);


        //запрос
        HttpResponseMessage response;
        try
        {
            response = await apiClient.PutAsync("changePassword", new ChangePasswordRequest(oldHashedPassword, newHashedPassword, repeatHashedPassword));
        }
        catch
        {
            return false;
        }

        //ответ
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        else return false;
    }

    public async Task LogoutAsync(string refreshToken, DeviceType deviceType, string? ip = null)
    {
        if (refreshToken == null || refreshToken == string.Empty) refreshToken = await tokenStorage.GetTokenAsync() ?? throw new Exception("Сессия не найдена");

        await apiClient.PostAsync("logout", new RefreshTokenRequest(refreshToken, (int)deviceType));

        await tokenStorage.SaveTokenAsync(null!);
        await apiClient.SetTokenAsync(null!);
    }
}