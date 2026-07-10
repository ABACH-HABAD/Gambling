namespace Gambling.Application.Core.Abstractions.Encryptions;

public interface IPasswordHasher
{
    public string HashPassword(string password);
}