using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Game;
using Gambling.Application.Core.Abstractions.Game.Slots;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core.BusinessModels.GameModels.Slots;
using Gambling.Application.Server.Services.Game;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Exceptions;
using Gambling.Core.Models;

namespace Gambling.Application.Server.Services.Game.Slots;

public class ServerSlotsService(
    IUserRepository userRepository,
    IGameRepository gameRepository,
    ISlotsCombinationCounterService slotsCombinationCounterService,
    ISlotsWinCounterService slotsWinCounterService)
    : ServerGameService(userRepository, gameRepository, GameType.Slots), IGameService, ISlotsService
{
    public async Task<bool> HasBonusGamesAsync(int userId)
    {
        UserModel? user = await _userRepository.GetWithIdAsync(userId);
        if (user != null)
        {
            return user.SlotsBonusCount > 0;
        }
        else return false;
    }

    public async Task<SlotGameResult?> SpinAsync(int userId, double bid, int linesCount = 3, int columnsCount = 5)
    {
        List<List<SlotElement>> lines = [];

        //Пытаемся списать ставку
        try
        {
            if (await HasBonusGamesAsync(userId)) await _userRepository.ChangeSlotsBonusGamesCountAsync(userId, -1);
            else  await _userRepository.WriteOffFromBalanceAsync(userId, bid);
        }
        catch (InsufficientFundsException)
        {
            return new SlotGameResult(false, "На балансе недостаточно средств", 0, lines);
        }
        catch (AccountNotFoundException)
        {
            return new SlotGameResult(false, $"Аккаунт не найден #{userId}", 0, lines);
        }

        UserModel? user = await _userRepository.GetWithIdAsync(userId);

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
        totalWin *= user == null ? 1 : user.Coefficient;

        await _userRepository.UpdateGameStatsAsync(userId, isGameWin: totalWin > 0, totalWin > 0 ? totalWin - bid : bid);
        await _userRepository.AddToBalanceAsync(userId, totalWin);
        await _userRepository.ChangeSlotsBonusGamesCountAsync(userId, totalBonusCount);

        await _gameRepository.AddGameAsync(userId, (int)DefaultGameType, bid, totalWin > 0, totalWin);

        return new SlotGameResult(true, $"Вы выиграли: {totalWin} рублей", totalWin, lines);
    }
}