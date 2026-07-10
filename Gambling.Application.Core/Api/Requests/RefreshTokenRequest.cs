namespace Gambling.Application.Core.Api.Requests;

public record RefreshTokenRequest(string Token, int DeviceType) : BaseRequest;