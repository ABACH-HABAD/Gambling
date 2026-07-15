using Gambling.Application.Core.Abstractions.Encryptions;
using Gambling.Application.Core.Abstractions.Token;

namespace Gambling.Application.Core.Services.Token;

public class AccessTokenStorageService(IEncryption encryption) : TokenStorageService("access.dat", encryption), ITokenStorageService;