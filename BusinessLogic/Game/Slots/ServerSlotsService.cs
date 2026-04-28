using DataBaseClasses;
using DataBaseClasses.Entity;
using DataBaseClasses.Repository;
using DataBaseClasses.Repository.Interfaces;

namespace BusinessLogic.Game.Slots;

public class ServerSlotsService(IUserRepository userRepository, IGameRepository gameRepository, ISlotsCombinationCounterService slotsCombinationCounterService, ISlotsWinCounterService slotsWinCounterService) : IGameService, ISlotsService
{
    private const int GAMETYPE = 1;
    public async Task<bool> HasBonusGames(int userId)
    {
        User? user = userRepository.GetWithId(userId);
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
            if (await HasBonusGames(userId)) userRepository.ChangeSlotsBonusGamesCount(userId, -1);
            else userRepository.WriteOffFromBalance(userId, bid);
        }
        catch (InsufficientFundsException)
        {
            return new SlotGameResult(false, "На балансе недостаточно средств", 0, lines);
        }
        catch (AccountNotFoundException)
        {
            return new SlotGameResult(false, $"Аккаунт не найден #{userId}", 0, lines);
        }

        User? user = userRepository.GetWithId(userId);

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

        userRepository.UpdateGameStats(userId, isGameWin: totalWin > 0, totalWin > 0 ? totalWin - bid : bid);
        userRepository.AddToBalance(userId, totalWin);
        userRepository.ChangeSlotsBonusGamesCount(userId, totalBonusCount);

        gameRepository.AddGame(userId, GAMETYPE, bid, totalWin > 0, totalWin);

        return new SlotGameResult(true, $"Вы выиграли: {totalWin} рублей", totalWin, lines);
    }
}
