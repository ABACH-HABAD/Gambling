namespace DataBaseClasses.Repository.Interfaces;

public interface IGameTypesRepository : ISeedable
{
    public bool HasThisGameType(int id);

    public bool HasThisGameType(string name);
}
