using BusinessLogic.Token;

namespace BusinessLogic.Account.Auth;

public record LoginResult(RefreshedTokens? Tokens, bool Result, string Message);