using Gambling.Core.Abstractions.Models;
using Gambling.Core.Models;

namespace Gambling.Infrastructure.Data.Entities;

internal class DeviceTypeEntity : BaseEntity<DeviceTypeEntity, DeviceTypeModel>, IImmutableRepositoryEntity
{
    public string Name { get; set; } = string.Empty;

    public KeyValuePair<int, string?> ToKeyPairs() => new(Id, Name);

    internal override DeviceTypeModel ToModel()
    {
        return new DeviceTypeModel() { Id = Id, Name = Name };
    }
}