using DataBaseClasses.Entity;
using DataBaseClasses.Repository.Interfaces;
using DataBaseClasses.Exceptions;
using Google.Protobuf.WellKnownTypes;
using System.Xml.Linq;

namespace DataBaseClasses.Repository;

public class UserRepository(ApplicationContext dataBaseContext) : BaseRepository(dataBaseContext), IUserRepository
{
    //Получает оригинал объекта User из DataSet базы данных. Используйте если надо обвновить данные в бд.
    protected User? InnerGetWithId(int id)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        return _dataBaseContext.Users.Find(id);
    }

    //Получает копию объекта User. Используйте когда надо, чтобы данные в бд были неизменными. Это также действително для всех публичных методов, возвращающих User.
    public User? GetWithId(int id)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        User user = (InnerGetWithId(id) ?? throw new AccountNotFoundException()).Clone<User>();
        user.Password = null; //пароль не передаётся для безопасности
        return user;
    }

    public bool ComparePassword(int userId, string passwordHash)
    {
        User user = InnerGetWithId(userId) ?? throw new AccountNotFoundException();
        return user.Password == passwordHash;
    }

    public User? FindByEmail(string email)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        User user = (_dataBaseContext.Users.FirstOrDefault(user => user.Email == email) ?? throw new AccountNotFoundException()).Clone<User>();
        user.Password = null;
        return user;
    }

    public User? Registrate(string email, string hashedPassword)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        User user = new()
        {
            Email = email,
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
        _dataBaseContext.Users.Add(user);
        _dataBaseContext.SaveChanges();
        return user;
    }

    public User? Login(string email, string hashedPassword)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        User? user = FindByEmail(email);
        if (user != null)
        {
            if (ComparePassword(user.Id, hashedPassword)) return user;
            else throw new IncorrectAccountDataException();
        }
        else throw new AccountNotFoundException();
    }

    public List<User> GetUserList()
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        return [.. _dataBaseContext.Users];
    }

    public void Update(User userData)
    {
        User user = InnerGetWithId(userData.Id) ?? throw new AccountNotFoundException();

        if (userData.Email != null) user.Email = userData.Email;
        if (userData.Password != null) user.Password = userData.Password;
        if (userData.Name != null) user.Name = userData.Name;
        if (userData.Status != 0) user.Status = userData.Status;
        if (userData.Coefficient != null) user.Coefficient = userData.Coefficient;

        _dataBaseContext.SaveChanges();
    }

    public void WriteOffFromBalance(int userId, double value)
    {
        User user = InnerGetWithId(userId) ?? throw new AccountNotFoundException();
        if (value < 0) throw new NotPossibleToDepositOrWithdrawNegativeAmountOfFundsException();
        if (user.Balance >= value) user.Balance -= value;
        else throw new InsufficientFundsException();

        _dataBaseContext.SaveChanges();
    }

    public void AddToBalance(int userId, double value)
    {
        User user = InnerGetWithId(userId) ?? throw new AccountNotFoundException();
        if (value < 0) throw new NotPossibleToDepositOrWithdrawNegativeAmountOfFundsException();
        user.Balance += value;

        _dataBaseContext.SaveChanges();
    }

    public void UpdateGameStats(int userId, bool isGameWin, double balanceChange)
    {
        User user = InnerGetWithId(userId) ?? throw new AccountNotFoundException();

        if (isGameWin)
        {
            user.WinCount ??= 0;
            user.WinCount++;

            user.WinBalance ??= 0;
            user.WinBalance += balanceChange;
        }
        else
        {
            user.LossCount ??= 0;
            user.LossCount++;

            user.LossBalance ??= 0;
            user.LossBalance += Math.Abs(balanceChange);
        }

        _dataBaseContext.SaveChanges();
    }

    public void ChangeSlotsBonusGamesCount(int userId, int slotBonusGamesChange)
    {
        User user = InnerGetWithId(userId) ?? throw new AccountNotFoundException();

        user.SlotsBonusCount += slotBonusGamesChange;

        _dataBaseContext.SaveChanges();
    }


}
