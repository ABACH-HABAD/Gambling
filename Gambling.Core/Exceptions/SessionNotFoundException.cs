using Gambling.Core.Models;

namespace Gambling.Core.Exceptions;

public class SessionNotFoundException : EntityNotFoundExcection
{
    public SessionNotFoundException() : base(typeof(SessionModel)) { }
}