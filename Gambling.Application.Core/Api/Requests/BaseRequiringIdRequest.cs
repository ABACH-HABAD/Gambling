namespace Gambling.Application.Core.Api.Requests;

public abstract record BaseRequiringIdRequest(int UserId) : BaseRequest;