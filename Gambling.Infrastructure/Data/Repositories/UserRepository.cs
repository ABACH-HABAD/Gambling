using Microsoft.EntityFrameworkCore;
using Gambling.Infrastructure.Data.Projections;
using Gambling.Infrastructure.Data.Entities;
using Gambling.Core.Exceptions;
using Gambling.Core.Models;
using Gambling.Core.Abstractions.Repositories;

namespace Gambling.Infrastructure.Data.Repositories;

public class UserRepository(ApplicationContext dataBaseContext) : BaseRepository(dataBaseContext), IUserRepository
{
    /// <summary>
    /// Получает оригинал объекта UserEntity из DataSet базы данных. Используйте если надо обвновить данные в бд.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>возвращает UserEntity если найдёт в БД, с таким id</returns>
    /// <exception cref="NoConnectionException"></exception>
    private async Task<UserEntity?> InnerGetWithIdAsync(int id)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        return await _dataBaseContext.Users.FindAsync(id);
    }

    /// <summary>
    /// Получает копию объекта User (UserModel). 
    /// Используйте когда надо, чтобы данные в бд были неизменными. 
    /// Это также действително для всех публичных методов, возвращающих User.
    /// Пароль не передаётся для безопасности.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>возвращает UserModel если найдёт в БД, с таким id</returns>
    /// <exception cref="NoConnectionException"></exception>
    /// <exception cref="AccountNotFoundException"></exception>
    public async Task<UserModel?> GetWithIdAsync(int id)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        UserModel? user = await _dataBaseContext.Users
        .ToUserModel()
        .FirstOrDefaultAsync(user => user.Id == id);

        return user;
    }

    public async Task<bool> ComparePasswordAsync(int userId, string passwordHash)
    {
        UserEntity user = await InnerGetWithIdAsync(userId) ?? throw new AccountNotFoundException();
        return user.Password == passwordHash;
    }

    public async Task<UserModel?> FindByEmailAsync(string email)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        UserModel? user = await _dataBaseContext.Users
        .ToUserModel()
        .FirstOrDefaultAsync(user => user.Email == email);
        return user;
    }

    public async Task<UserModel?> RegistrateAsync(string email, string hashedPassword)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        UserEntity user = new()
        {
            Email = email,
            Name = string.Empty,
            StatusId = 1,
            Password = hashedPassword,
            IsBlocked = false,
            Coefficient = 1,
            Balance = 0,
            WinBalance = 0,
            WinCount = 0,
            LossBalance = 0,
            LossCount = 0,
            SlotsBonusCount = 0,
        };


        await _dataBaseContext.Users.AddAsync(user);
        await _dataBaseContext.SaveChangesAsync();

        UserModel dto = user.ToModel();
        dto.Status = new UserStatusModel() { Id = 1, Name = "Пользователь" };
        return dto;
    }

    public async Task<UserModel?> LoginAsyncAsync(string email, string hashedPassword)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        UserModel? user = await FindByEmailAsync(email);
        if (user != null)
        {
            if (await ComparePasswordAsync(user.Id, hashedPassword)) return user;
            else throw new IncorrectAccountDataException();
        }
        else throw new AccountNotFoundException();
    }

    public async Task<List<UserModel>> GetUserListAsync()
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        List<UserModel> users = await _dataBaseContext.Users
        .ToUserModel()
        .ToListAsync();

        return users;
    }

    public async Task ChangeEmailAsync(int userId, string newEmail)
    {
        UserEntity user = await InnerGetWithIdAsync(userId) ?? throw new AccountNotFoundException();

        user.Email = newEmail;

        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task ChangePasswordAsync(int userId, string hashedPassword)
    {
        UserEntity user = await InnerGetWithIdAsync(userId) ?? throw new AccountNotFoundException();

        user.Password = hashedPassword;

        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task ChangeNameAsync(int userId, string newName)
    {
        UserEntity user = await InnerGetWithIdAsync(userId) ?? throw new AccountNotFoundException();

        user.Name = newName;

        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(int userId, int statusId)
    {
        UserEntity user = await InnerGetWithIdAsync(userId) ?? throw new AccountNotFoundException();

        user.StatusId = statusId;

        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task ChangeIsBlockedAsync(int userId, bool isBlocked)
    {
        UserEntity user = await InnerGetWithIdAsync(userId) ?? throw new AccountNotFoundException();

        user.IsBlocked = isBlocked;

        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task WriteOffFromBalanceAsync(int userId, double value)
    {
        UserEntity user = await InnerGetWithIdAsync(userId) ?? throw new AccountNotFoundException();
        if (value < 0) throw new NotPossibleToDepositOrWithdrawNegativeAmountOfFundsException();
        if (user.Balance >= value) user.Balance -= value;
        else throw new InsufficientFundsException();

        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task AddToBalanceAsync(int userId, double value)
    {
        UserEntity user = await InnerGetWithIdAsync(userId) ?? throw new AccountNotFoundException();
        if (value < 0) throw new NotPossibleToDepositOrWithdrawNegativeAmountOfFundsException();
        user.Balance += value;

        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task UpdateGameStatsAsync(int userId, bool isGameWin, double balanceChange)
    {
        UserEntity user = await InnerGetWithIdAsync(userId) ?? throw new AccountNotFoundException();

        if (isGameWin)
        {
            user.WinCount++;
            user.WinBalance += balanceChange;
        }
        else
        {
            user.LossCount++;
            user.LossBalance += Math.Abs(balanceChange);
        }

        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task ChangeSlotsBonusGamesCountAsync(int userId, int slotBonusGamesChange)
    {
        UserEntity user = await InnerGetWithIdAsync(userId) ?? throw new AccountNotFoundException();

        user.SlotsBonusCount += slotBonusGamesChange;

        await _dataBaseContext.SaveChangesAsync();
    }
}