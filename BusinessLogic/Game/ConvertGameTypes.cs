namespace BusinessLogic.Game;

public static class ConvertGameTypes
{
    public static GameType ToGameType(this DataBaseClasses.Entity.GameType type) => (GameType)type.Id;
}