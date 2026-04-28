using BusinessLogic.Token;

namespace BusinessLogic.Auth;

public record LoginResult(RefreshedTokens? Tokens, bool Result, string Message);