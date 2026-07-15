namespace Gambling.Application.Core.Api.Requests;

public record UnblockRequest(int UserId) : BaseRequiringIdRequest(UserId);