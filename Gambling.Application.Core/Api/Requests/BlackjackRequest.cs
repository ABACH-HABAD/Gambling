namespace Gambling.Application.Core.Api.Requests;

public record BlackjackRequest(int UserId, double Bet) : BaseRequiringIdRequest(UserId);