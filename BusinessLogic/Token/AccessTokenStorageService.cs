using BusinessLogic.Encryption;

namespace BusinessLogic.Token;

public class AccessTokenStorageService(IEncryption encryption) : TokenStorageService("access.dat", encryption), ITokenStorageService;