using System.Security.Claims;

namespace Gambling.Application.Core.Abstractions.Encryptions;

public interface IUserIdExtraction
{
    public int? ExtractUserId(Claim userIdClaim);
}