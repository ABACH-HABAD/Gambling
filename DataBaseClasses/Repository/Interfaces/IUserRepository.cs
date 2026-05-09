using DataBaseClasses.Entity;

namespace DataBaseClasses.Repository.Interfaces;

public interface IUserRepository : IRepository
{
    public User? GetWithId(int id);

    public User? FindByEmail(string email);

    public User? Registrate(string email, string hashedPassword);

    public User? Login(string username, string hashedPassword);

    public List<User> GetUserList();

    public void Update(User user);

    public void WriteOffFromBalance(int userId, double value);

    public void AddToBalance(int userId, double value);

    public void UpdateGameStats(int userId, bool isGameWin, double balanceChange);

    public void ChangeSlotsBonusGamesCount(int userId, int slotBonusGamesChange);
}