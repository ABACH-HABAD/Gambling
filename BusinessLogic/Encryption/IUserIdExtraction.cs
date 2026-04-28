using System.Security.Claims;

namespace BusinessLogic.Encryption;

public interface IUserIdExtraction
{
    public int? ExtractUserId(Claim userIdClaim);
}
