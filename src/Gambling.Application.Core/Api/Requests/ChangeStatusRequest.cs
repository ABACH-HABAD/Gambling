namespace Gambling.Application.Core.Api.Requests;

public record ChangeStatusRequest(int UserId, int StatusId) : BaseRequiringIdRequest(UserId);