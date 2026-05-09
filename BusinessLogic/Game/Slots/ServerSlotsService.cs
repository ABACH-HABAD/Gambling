using DataBaseClasses.Entity;
using DataBaseClasses.Exceptions;
using DataBaseClasses.Repository.Interfaces;

namespace BusinessLogic.Game.Slots;

public class ServerSlotsService(
    IUserRepository userRepository,
    IGameRepository gameRepository,
    ISlotsCombinationCounterService slotsCombinationCounterService,
    ISlotsWinCounterService slotsWinCounterService)
    : ServerGameService(userRepository, gameRepository, GameType.Slots), IGameService, ISlotsService
{
    public async Task<bool> HasBonusGames(int userId)
    {
        User? user = _userRepository.GetWithId(userId);
        if (user != null)
        {
            return user.SlotsBonusCount > 0;
        }
        else return false;
    }

    public async Task<SlotGameResult?> Spin(int userId, double bid, int linesCount = 3, int columnsCount = 5)
    {
        List<List<SlotElement>> lines = [];

        //Пытаемся списать ставку
        try
        {
            if (await HasBonusGames(userId)) _userRepository.ChangeSlotsBonusGamesCount(userId, -1);
            else _userRepository.WriteOffFromBalance(userId, bid);
        }
        catch (InsufficientFundsException)
        {
            return new SlotGameResult(false, "На балансе недостаточно средств", 0, lines);
        }
        catch (AccountNotFoundException)
        {
            return new SlotGameResult(false, $"Аккаунт не найден #{userId}", 0, lines);
        }

        User? user = _userRepository.GetWithId(userId);

        int totalBonusCount = 0;
        double totalWin = 0;

        //Прокручиваем линии
        for (int line = 0; line < linesCount; line++)
        {
            List<SlotElement> slots = [];
            lines.Add(slots);

            //Крутим 5 барабанов
            for (int i = 0; i < columnsCount; i++)
            {
                slots.Add(new());
            }
        }

        totalWin +=
            slotsWinCounterService.TotalWinCount(
                slotsCombinationCounterService.LinesComboCount(lines));

        totalWin +=
            slotsWinCounterService.TotalWinCount(
                slotsCombinationCounterService.ColumnsComboCount(lines));

        totalBonusCount +=
            slotsWinCounterService.BonusCount(
                slotsCombinationCounterService.LinesComboCount(lines));

        totalWin *= bid;
        totalWin *= user == null ? 1 : (user.Coefficient ?? 1);

        _userRepository.UpdateGameStats(userId, isGameWin: totalWin > 0, totalWin > 0 ? totalWin - bid : bid);
        _userRepository.AddToBalance(userId, totalWin);
        _userRepository.ChangeSlotsBonusGamesCount(userId, totalBonusCount);

        _gameRepository.AddGame(userId, (int)DefaultGameType, bid, totalWin > 0, totalWin);

        return new SlotGameResult(true, $"Вы выиграли: {totalWin} рублей", totalWin, lines);
    }
}
