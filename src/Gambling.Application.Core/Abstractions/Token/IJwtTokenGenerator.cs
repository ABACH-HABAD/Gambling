using Gambling.Core.Models;

namespace Gambling.Application.Core.Abstractions.Token;

public interface IJwtTokenGenerator
{
    public string GenerateAccessJwtToken(UserModel user);

    public string GenerateRefreshJwtToken();
}