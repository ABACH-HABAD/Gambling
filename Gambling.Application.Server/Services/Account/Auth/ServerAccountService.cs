using MySql.Data.MySqlClient;
using Gambling.Core.Exceptions;
using Gambling.Core.Models;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Sessions;
using Gambling.Application.Core.Abstractions.Validation;
using Gambling.Application.Core.Abstractions.Token;
using Gambling.Application.Core;

namespace Gambling.Application.Server.Services.Account.Auth;

public class ServerAccountService(
    IEmailValidation emailValidation,
    IUserRepository userRepository,
    ISessionService sessionService,
    IJwtTokenGenerator jwtTokenGenerator) : IAccountService
{
    public async Task<LoginResult> RegistrateAsync(string login, string hasedPassword, string repeatPassword, DeviceType deviceType, string? ip = null)
    {
        //Валидация
        if (!emailValidation.Validate(login, out string loginError)) return new LoginResult(null, false, loginError);
        if (hasedPassword != repeatPassword) return new LoginResult(null, false, "Пароли не совпадают");

        try
        {
            if ((await CheckRegistrationAsync(login)))
                return new LoginResult(null, false, "Аккаунт с таким логином уже существует");
        }
        catch (NoConnectionException)
        {
            return new LoginResult(null, false, "Нет подключения к базе данных");
        }

        //Запрос к БД
        try
        {
            UserModel? user = await userRepository.RegistrateAsync(login, hasedPassword);
            if (user == null) return new LoginResult(null, false, "Аккаунт не найден");
            else
            {
                string refreshToken = jwtTokenGenerator.GenerateRefreshJwtToken();
                string accessToken = jwtTokenGenerator.GenerateAccessJwtToken(user);
                await sessionService.Login(user, refreshToken, (int)deviceType, ip);

                return new LoginResult(new(accessToken, refreshToken), true, "Аккаунт успешно зарегестрирован");
            }
        }
        catch (NoConnectionException)
        {
            return new LoginResult(null, false, "Нет подключения к базе данных");
        }
        catch (Exception ex)
        {
            return new LoginResult(null, false, ex.Message);
        }
    }

    public async Task<LoginResult> LoginAsync(string login, string hasedPassword, DeviceType deviceType, string? ip = null, bool loginAsAdmin = false)
    {
        //Валидация
        if (!emailValidation.Validate(login, out string loginError)) return new LoginResult(null, false, loginError);


        //Запрос к БД
        try
        {
            UserModel? user = await userRepository.LoginAsyncAsync(login, hasedPassword) ?? throw new IncorrectAccountDataException();
            if (loginAsAdmin && user.Status!.Id != 3) throw new InsufficientRightsException();

            string refreshToken = jwtTokenGenerator.GenerateRefreshJwtToken();
            string accessToken = jwtTokenGenerator.GenerateAccessJwtToken(user);
            await sessionService.Login(user, refreshToken, (int)deviceType, ip);

            return new LoginResult(new(accessToken, refreshToken), true, "Вы успешно авторизированы");

        }
        catch (IncorrectAccountDataException)
        {
            return new LoginResult(null, false, "Неверный логин или пароль");
        }
        catch (InsufficientRightsException)
        {
            return new LoginResult(null, false, "Вход разрешён только для администратора");
        }
        catch (AccountNotFoundException)
        {
            return new LoginResult(null, false, "Аккаунт не найден");
        }
        catch (NoConnectionException)
        {
            return new LoginResult(null, false, "Нет подключения к базе данных");
        }
        catch (MySqlException ex)
        {
            return new LoginResult(null, false, $"Ошибка подключения к базе данных: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new LoginResult(null, false, $"Произошла непредвиденная ошибка {ex.GetType()}: {ex.Message}");
        }
    }

    public async Task<LoginResult> AutoLoginAsync(string refreshToken, DeviceType deviceType, string? ip)
    {
        try
        {
            SessionModel? session = await sessionService.GetSessionAsync(refreshToken, ip);
            if (session != null && session.UserId is int userId)
            {
                UserModel? user = await userRepository.GetWithIdAsync(userId);

                if (user != null)
                {
                    string accessToken = jwtTokenGenerator.GenerateAccessJwtToken(user);
                    await sessionService.Login(user, refreshToken, (int)deviceType, ip);

                    return new LoginResult(new(accessToken, refreshToken), true, "Вы успешно авторизированы");
                }
                else return new LoginResult(null, false, "Аккаунт не найден");
            }
            else return new LoginResult(null, false, "Аккаунт не найден");
        }
        catch (AccountNotFoundException)
        {
            return new LoginResult(null, false, "Аккаунт не найден");
        }
        catch (NoConnectionException)
        {
            return new LoginResult(null, false, "Нет подключения к базе данных");
        }
        catch (MySqlException ex)
        {
            return new LoginResult(null, false, $"Ошибка подключения к базе данных: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new LoginResult(null, false, $"Произошла непредвиденная ошибка {ex.GetType()}: {ex.Message}");
        }
    }

    public async Task<bool> ChangeEmailAsync(int userId, string oldEmail, string newEmail)
    {
        if (!emailValidation.Validate(oldEmail, out string error1)) throw new ValidationException(error1);
        if (!emailValidation.Validate(newEmail, out string error2)) throw new ValidationException(error2);

        UserModel? user = await userRepository.GetWithIdAsync(userId);
        if (user != null && user.Email == oldEmail)
        {
            await userRepository.ChangeEmailAsync(userId, newEmail);
            return true;
        }
        else throw new Exception("Аккаунт с такой электронной почтой не найден");
    }

    public async Task<bool> ChangePasswordAsync(int userId, string oldHashedPassword, string newHashedPassword, string repeatHashedPassword, bool forceChange = false)
    {
        if (newHashedPassword != repeatHashedPassword) throw new ValidationException("Пароли не совпадают");

        if (forceChange || await userRepository.ComparePasswordAsync(userId, oldHashedPassword))
        {
            await userRepository.ChangePasswordAsync(userId, newHashedPassword);
            return true;
        }
        else throw new Exception("Неверный пароль от аккаунта");
    }

    public async Task LogoutAsync(string refreshToken, DeviceType deviceType, string? ip)
    {
        await sessionService.Logout(refreshToken, (int)deviceType, ip);
    }

    public async Task<bool> CheckRegistrationAsync(string login)
    {
        try
        {
            UserModel? user = await userRepository.FindByEmailAsync(login);
            return user != null;
        }
        catch (AccountNotFoundException)
        {
            return false;
        }
    }
}