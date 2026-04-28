using BusinessLogic.Auth.Validation;
using BusinessLogic.Token;
using DataBaseClasses;
using DataBaseClasses.Entity;
using DataBaseClasses.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;

namespace BusinessLogic.Auth
{
    public class ServerAccountService(
        IValidation emailValidation,
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
                User? user = userRepository.Registrate(login, hasedPassword);
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
                User user = userRepository.Login(login, hasedPassword) ?? throw new AccountNotFoundException();
                if (loginAsAdmin && user.Status != 3) return new LoginResult(null, false, "Для входа требуется аккаунт администратора");

                string refreshToken = jwtTokenGenerator.GenerateRefreshJwtToken();
                string accessToken = jwtTokenGenerator.GenerateAccessJwtToken(user);
                await sessionService.Login(user, refreshToken, (int)deviceType, ip);

                return new LoginResult(new(accessToken, refreshToken), true, "Вы успешно авторизированы");

            }
            catch (IncorrectAccountDataException)
            {
                return new LoginResult(null, false, "Неверный логин или пароль");
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
                Session? session = await sessionService.GetSessionAsync(refreshToken, ip);
                if (session != null && session.User is int userId)
                {
                    User? user = userRepository.GetWithId(userId);

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
                return new LoginResult(null, false, "Нет аккаунта");
            }
        }

        public async Task<bool> CheckRegistrationAsync(string login)
        {
            try
            {
                User? user = userRepository.FindByEmail(login);
                return user != null;
            }
            catch (AccountNotFoundException)
            {
                return false; ;
            }
        }

        public async Task<User?> GetUserData(int userId)
        {
            User? user = userRepository.GetWithId(userId);
            if (user != null)
            {
                user.Password = null;
                return user;
            }
            else return null;
        }

        public async Task<User?> UpdateUserDataAsync(User user)
        {
            userRepository.Update(user);

            return await GetUserData(user.Id);
        }

        public async Task<bool> TopUpBalance(int userId, double sum)
        {
            try
            {
                userRepository.AddToBalance(userId, sum);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
