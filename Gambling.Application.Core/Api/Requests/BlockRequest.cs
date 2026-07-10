namespace Gambling.Application.Core.Api.Requests;

public record BlockRequest(int UserId) : BaseRequiringIdRequest(UserId);