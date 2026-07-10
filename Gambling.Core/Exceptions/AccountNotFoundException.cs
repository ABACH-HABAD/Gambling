using Gambling.Core.Models;

namespace Gambling.Core.Exceptions;

public class AccountNotFoundException : EntityNotFoundExcection
{
    public AccountNotFoundException() : base(typeof(UserModel)) { }
}