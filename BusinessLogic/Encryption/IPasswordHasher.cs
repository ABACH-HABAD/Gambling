namespace BusinessLogic.Encryption;

public interface IPasswordHasher
{
    public string HashPassword(string password);
}
