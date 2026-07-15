namespace Gambling.Application.Core.Api.Requests;

public record ChangeBalanceRequest(int UserId, double Sum) : BaseRequiringIdRequest(UserId);