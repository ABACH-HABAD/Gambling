namespace Gambling.Application.Core.Api.Results;

public record PromocionalCodeActivateResult(bool IsActivateSucces, double ChangeBalance, int FreeSpinsChange);