namespace Gambling.Application.Core.Api.Requests;

public record ChangeEmailRequest(int UserId, string OldEmail, string NewEmail) : BaseRequiringIdRequest(UserId);