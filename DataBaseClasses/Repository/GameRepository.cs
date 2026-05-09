using DataBaseClasses.Entity;
using DataBaseClasses.Repository.Interfaces;
using DataBaseClasses.Exceptions;

namespace DataBaseClasses.Repository;

public class GameRepository(ApplicationContext context, IGameTypesRepository gameTypesRepository) : BaseRepository(context), IGameRepository
{
    protected Game? GetWithId(int id)
    {
        if (!CheckConncetion()) throw new NoConnectionException();

        return _dataBaseContext.Games.Find(id);
    }

    public void AddGame(int playerId, int gameType, double bet, bool isWin, double winAmount)
    {
        if (!gameTypesRepository.HasThisGameType(gameType)) throw new ArgumentException("Укажите действительный тип игры", nameof(gameType));

        Game game = new()
        {
            Player = playerId,
            GameType = gameType,
            Bid = bet,
            IsWin = isWin,
            WinAmount = winAmount
        };

        _dataBaseContext.Games.Add(game);

        _dataBaseContext.SaveChanges();
    }

    public void AddGame(Game game)
    {
        if (game.Bid == null) throw new ArgumentException("Ставка обязательно должа быть задана", nameof(game));
        if (game.IsWin == null) throw new ArgumentException("Победа должна быть задана", nameof(game));
        if (game.Player == null) throw new ArgumentException("Игрок обязательно должен быть задан", nameof(game));
        if (game.GameType == null) throw new ArgumentException("Тип игры обязательно должен быть задан", nameof(game));
        if (game.GameType is int gameType && gameTypesRepository.HasThisGameType(gameType)) throw new ArgumentException("Укажите действительный тип игры", nameof(game));

        _dataBaseContext.Games.Add(game);

        _dataBaseContext.SaveChanges();
    }

    public List<Game> GetGames() => [.. _dataBaseContext.Games];
    public List<Game> GetGames(int gameType) => [.. _dataBaseContext.Games.Where(game => game.GameType == gameType)];
    public List<Game> GetGames(int playerId, int gameType, bool isWin) => [.. _dataBaseContext.Games.Where(game => game.Player == playerId && game.GameType == gameType && game.IsWin == isWin)];
}
