using Gambling.Application.Core.Abstractions.Encryptions;
using Gambling.Application.Core.Abstractions.Token;

namespace Gambling.Application.Core.Services.Token;

public class RefreshTokenStorageService(IEncryption encryption) : TokenStorageService("refresh.dat", encryption), ITokenStorageService;