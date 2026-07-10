using Gambling.Core.Abstractions.Models;
using Gambling.Core.Models;

namespace Gambling.Infrastructure.Data.Entities;

internal class GameTypeEntity : BaseEntity<GameTypeEntity, GameTypeModel>, IImmutableRepositoryEntity
{
    public string Name { get; set; } = string.Empty;

    public KeyValuePair<int, string?> ToKeyPairs() => new(Id, Name);

    internal override GameTypeModel ToModel()
    {
        return new GameTypeModel() { Id = Id, Name = Name };
    }
}