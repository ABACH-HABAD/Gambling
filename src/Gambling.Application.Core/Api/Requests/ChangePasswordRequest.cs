namespace Gambling.Application.Core.Api.Requests;

public record ChangePasswordRequest(int UserId, string OldHashedPassword, string NewHashedPassword, string RepeatHashedPassword, bool ForceChange = false) : BaseRequiringIdRequest(UserId);