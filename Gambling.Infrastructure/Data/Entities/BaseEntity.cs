using Gambling.Core.Abstractions.Models;
using Gambling.Core.Models;

namespace Gambling.Infrastructure.Data.Entities;

internal abstract class BaseEntity<TEntity, TModel> : AbstractEntity, IIdable where TEntity : AbstractEntity where TModel : BaseModel
{
    internal TEntity Clone()
    {
        return (TEntity)MemberwiseClone();
    }

    internal abstract TModel ToModel();
}