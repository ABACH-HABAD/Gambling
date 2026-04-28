using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.Game.Slots;

namespace TestProject.Games.Slots;

public class SlotsWinTests : DependencyOnServicesTest
{
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await InitializeAsync();
    }

    [Test]
    public async Task ComboWhith5Elements()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        ISlotsWinCounterService slotsWinCounterService = _serviceProvider.GetRequiredService<ISlotsWinCounterService>();
        List<SlotElement> elements =
            [
                new("Вишня"), new("Вишня"), new("Вишня"), new("Вишня"), new("Вишня")
            ];
        double coefficient = 7.5;
        double count = 5;
        double requiredWin = count * coefficient;

        //Act
        double realWin = slotsWinCounterService.WinCount(slotsCombinationCounterService.ComboCount(elements));

        //Assert
        if (requiredWin == realWin) Assert.Pass("Всё верно");
        else Assert.Fail($"Вsигрышь составил: {realWin}");
    }
}
