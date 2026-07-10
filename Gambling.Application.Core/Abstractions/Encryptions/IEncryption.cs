namespace Gambling.Application.Core.Abstractions.Encryptions;

public interface IEncryption
{
    public byte[] Encrypt(string data);
    public string Decrypt(byte[] bytes);
}
