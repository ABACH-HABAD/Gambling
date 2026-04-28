using DataBaseClasses.Entity;

namespace BusinessLogic.Token;

public interface IJwtTokenGenerator
{
    public string GenerateAccessJwtToken(User user);

    public string GenerateRefreshJwtToken();
}
