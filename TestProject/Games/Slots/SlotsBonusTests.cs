using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.Game.Slots;

namespace TestProject.Games.Slots;

public class SlotsBonusTests : DependencyOnServicesTest
{
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await InitializeAsync(Type.Server);
    }

    [Test]
    public async Task Bonus4And1WildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 4 + 25) Assert.Pass($"Комбо собрано: {combo.Symbol}");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus4And1WildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.WILD), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 4 + 25) Assert.Pass($"Комбо собрано: {combo.Symbol}");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus3And2WildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 3 + 25) Assert.Pass($"Комбо собрано: {combo.Symbol}");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus3And2WildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 3 +25) Assert.Pass($"Комбо собрано: {combo.Symbol}");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2And3WildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 2 + 25) Assert.Pass($"Комбо собрано: {combo.Symbol}");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2And3WildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.BONUS), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 2 + 25) Assert.Pass($"Комбо собрано: {combo.Symbol}");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus1And4WildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.BONUS), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 1 + 25) Assert.Pass($"Комбо собрано: {combo.Symbol}");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus1And4WildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 1 + 25) Assert.Pass($"Комбо собрано: {combo.Symbol}");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus5()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == SlotElement.BONUS && combo.BonusCount == 5 + 25) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus4InStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS),  new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 4 + 16) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus4InEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("Лемон"), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 4 + 16) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus4InPositions1345()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.BONUS), new("Лемон"), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 4 + 9) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus4InPositions1245()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.BONUS), new(SlotElement.BONUS), new("Лемон"), new(SlotElement.BONUS), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 4) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus4InPositions1235()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS), new("Лемон"), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 4+ 9) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus3InStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS), new("Лемон"), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 3 + 9) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus3InCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("Лемон"), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 3 + 9) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus3InEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("Лемон"), new("Лемон"), new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 3 + 9) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.BONUS), new(SlotElement.BONUS), new("Лемон"), new("Лемон"), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 2) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new("Лемон"), new("Лемон"), new(SlotElement.BONUS), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 2) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InPositions1And3()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new(SlotElement.BONUS), new("Лемон"), new(SlotElement.BONUS), new("Лемон"), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 2) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InPositions1And4()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new(SlotElement.BONUS), new("Лемон"), new("Лемон"), new(SlotElement.BONUS), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 2) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InPositions1And5()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new(SlotElement.BONUS), new("Лемон"), new("Лемон"), new("Лемон"), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 2) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InPositions2And5()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new(SlotElement.BONUS), new("Лемон"), new("Лемон"), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 2) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InPositions2And4()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new(SlotElement.BONUS), new("Лемон"), new(SlotElement.BONUS), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 2) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InPositions2And3()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new(SlotElement.BONUS), new(SlotElement.BONUS), new("Лемон"), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 2) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InPositions3And4()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new("Лемон"), new(SlotElement.BONUS), new(SlotElement.BONUS), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 2) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InPositions3And5()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new("Лемон"), new(SlotElement.BONUS), new("Лемон"), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 2) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task BonusInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new(SlotElement.BONUS), new("Лемон"), new("Лемон"), new("Лемон"), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 1) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task BonusInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new("Лемон"), new("Лемон"), new("Лемон"), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 1) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task BonusInPosition2()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new(SlotElement.BONUS), new("Лемон"), new("Лемон"), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 1) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task BonusInPosition3()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new("Лемон"), new(SlotElement.BONUS), new("Лемон"), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 1) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task BonusInPosition4()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new("Лемон"), new("Лемон"), new(SlotElement.BONUS), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.BonusCount == 1) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InStartAndWildInCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.WILD), new("Лемон"), new("Лемон")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 2 + 9) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InEndAndWildInCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new("Лемон"), new(SlotElement.WILD), new(SlotElement.BONUS), new(SlotElement.BONUS),
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 2 + 9) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InEndAnd2WildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.BONUS), new(SlotElement.BONUS),
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 2 + 16) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task Bonus2InStartAnd2WildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new(SlotElement.BONUS), new(SlotElement.BONUS), new(SlotElement.WILD), new(SlotElement.WILD), new("Лемон"),
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 2 + 16) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task BonusInStartAnd3WildInCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new(SlotElement.BONUS), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new("Лемон"),
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 1 + 16) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task BonusInEndAnd3WildInCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Лемон"), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.BONUS)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Symbol == SlotElement.BONUS && combo.BonusCount == 1 + 16) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }
}
