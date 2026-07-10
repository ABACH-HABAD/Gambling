using Gambling.Core.Abstractions.Models;

namespace Gambling.Core.Models;

public class GameTypeModel : BaseModel, IImmutableRepositoryEntity
{
    public string Name { get; set; } = string.Empty;

    public KeyValuePair<int, string?> ToKeyPairs() => new(Id, Name);
}