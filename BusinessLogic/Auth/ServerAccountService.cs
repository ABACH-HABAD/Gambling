using BusinessLogic.Balance;
using BusinessLogic.Exceptions;
using BusinessLogic.Token;
using BusinessLogic.Validation;
using DataBaseClasses.Entity;
using DataBaseClasses.Exceptions;
using DataBaseClasses.Repository.Interfaces;
using MySql.Data.MySqlClient;

namespace BusinessLogic.Auth
{
    public class ServerAccountService(
        IBalanceService balanceService,
        IValidation emailValidation,
        IUserRepository userRepository,
        ISessionService sessionService,
        IJwtTokenGenerator jwtTokenGenerator) : IAccountService
    {
        public async Task<LoginResult> RegistrateAsync(string login, string hasedPassword, string repeatPassword, DeviceType deviceType, string? ip = null)
        {
            if (emailValidation is not EmailValidation) throw new Exception("Ты даун?");
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
                User? user = userRepository.Login(login, hasedPassword) ?? throw new IncorrectAccountDataException();
                if (loginAsAdmin && user.Status != 3) throw new InsufficientRightsException();

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

        public async Task LogoutAsync(string refreshToken, DeviceType deviceType, string? ip)
        {
            await sessionService.Logout(refreshToken, (int)deviceType, ip);
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

        public async Task<User?> GetUserDataAsync(int userId)
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

            return await GetUserDataAsync(user.Id);
        }

        public async Task<bool> ChangeBalanceAsync(int userId, double sum)
        {
            if (sum == 0) return false;
            try
            {
                if (sum > 0) return await balanceService.AddToBalanceAsync(userId, sum);
                else return await balanceService.RemoveFromBalanceAsync(userId, sum);
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<User>> GetAllUsersAsync(int adminId)
        {
            User? admin = userRepository.GetWithId(adminId);
            if (admin == null || admin.Status != 3) throw new YouDoNotHavePermissionException();

            return userRepository.GetUserList();
        }
    }
}
