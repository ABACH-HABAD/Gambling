namespace Gambling.Application.Core.Api.Requests;

public record LoginRequest(string Login, string Password, int DeviceType, bool? LoginAsAdmin) : BaseRequest;