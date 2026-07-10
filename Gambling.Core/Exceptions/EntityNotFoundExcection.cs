namespace Gambling.Core.Exceptions;

public class EntityNotFoundExcection(Type type) : DataBaseException
{
    public Type EntityType { get; init; } = type;
}