namespace Gambling.Application.Core.Api.Requests;

public record ChangeNameRequest(int UserId, string Name) : BaseRequiringIdRequest(UserId);