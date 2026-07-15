using Gambling.Application.Core.Services.Token;

namespace Gambling.Application.Core.Api.Results;

public record LoginResult(RefreshedTokens? Tokens, bool Result, string Message);