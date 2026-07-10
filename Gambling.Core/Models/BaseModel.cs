using Gambling.Core.Abstractions.Models;

namespace Gambling.Core.Models;

public class BaseModel : IIdable
{
    public required int Id { get; set; }
}