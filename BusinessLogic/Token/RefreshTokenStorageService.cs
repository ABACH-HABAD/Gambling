using BusinessLogic.Encryption;

namespace BusinessLogic.Token;

public class RefreshTokenStorageService(IEncryption encryption) : TokenStorageService("refresh.dat", encryption), ITokenStorageService;