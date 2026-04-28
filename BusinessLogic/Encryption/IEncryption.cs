using DataBaseClasses.Entity;
using System.Security.Claims;

namespace BusinessLogic.Encryption;

public interface IEncryption
{
    public byte[] Encrypt(string data);
    public string Decrypt(byte[] bytes);
}
