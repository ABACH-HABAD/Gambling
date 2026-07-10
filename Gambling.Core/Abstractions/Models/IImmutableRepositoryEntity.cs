namespace Gambling.Core.Abstractions.Models;

public interface IImmutableRepositoryEntity : INameable
{
    public KeyValuePair<int, string?> ToKeyPairs();
}