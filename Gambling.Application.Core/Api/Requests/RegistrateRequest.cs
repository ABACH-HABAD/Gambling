namespace Gambling.Application.Core.Api.Requests;

public record RegistrateRequest(string Login, string Password, string RepeatPassword, int DeviceType): BaseRequest;