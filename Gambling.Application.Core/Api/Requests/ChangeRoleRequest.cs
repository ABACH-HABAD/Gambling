namespace Gambling.Application.Core.Api.Requests;

public record ChangeRoleRequest(int UserId, int StatusId) : BaseRequiringIdRequest(UserId);