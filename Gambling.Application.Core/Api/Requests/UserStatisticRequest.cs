namespace Gambling.Application.Core.Api.Requests;

public record UserStatisticRequest(int UserId,int GameType) : BaseRequiringIdRequest(UserId);