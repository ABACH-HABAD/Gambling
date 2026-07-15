using Gambling.Core.Models;

namespace Gambling.Application.Core.Services.Game;

public static class ConvertGameTypes
{
    public static GameType ToGameType(this GameTypeModel type) => (GameType)type.Id;
}