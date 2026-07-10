namespace Gambling.Core.Abstractions.Repositories;

public interface IGameTypesRepository : ISeedableRepository
{
    public bool HasThisGameType(int id);
    public bool HasThisGameType(string name);
}