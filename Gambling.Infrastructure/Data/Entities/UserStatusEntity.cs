using Gambling.Core.Abstractions.Models;
using Gambling.Core.Models;

namespace Gambling.Infrastructure.Data.Entities;

internal class UserStatusEntity : BaseEntity<UserStatusEntity, UserStatusModel>, IImmutableRepositoryEntity
{
    public string Name { get; set; } = string.Empty;

    public KeyValuePair<int, string?> ToKeyPairs() => new(Id, Name);

    internal override UserStatusModel ToModel()
    {
        return new UserStatusModel { Id = Id, Name = Name };
    }
}