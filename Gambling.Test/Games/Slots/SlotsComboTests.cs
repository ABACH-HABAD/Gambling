using Microsoft.Extensions.DependencyInjection;
using Gambling.Application.Core.Abstractions.Game.Slots;
using Gambling.Application.Core.BusinessModels.GameModels.Slots;

namespace Gambling.Test.Games.Slots;

public class SlotsComboTests : DependencyOnServicesTest
{
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await InitializeAsync(Type.Server);
    }

    [Test]
    public async Task ComboWhith5Elements()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("Вишня"), new("Вишня"), new("Вишня"), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("Вишня"), new("Вишня"), new("Вишня"), new("Вишня"), new("BAR")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("BAR"), new("Вишня"), new("Вишня"), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("Вишня"), new("Вишня"), new("Вишня"), new("Арбуз"), new("BAR")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("BAR"), new("Вишня"), new("Вишня"), new("Вишня"), new("BAR")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("BAR"), new("Арбуз"), new("Вишня"), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhithAllWilds()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == SlotElement.WILD) Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd1WildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.WILD), new("Вишня"), new("Вишня"), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd1WildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("Вишня"), new("Вишня"), new("Вишня"), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd1WildInPosition2()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("Вишня"), new(SlotElement.WILD), new("Вишня"), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd1WildInPosition3()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("Вишня"), new("Вишня"), new(SlotElement.WILD), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd1WildInPosition4()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new("Вишня"), new("Вишня"), new("Вишня"), new(SlotElement.WILD), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd2WildsInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd2WildsInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Вишня"), new("Вишня"), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd2WildsInPositions1And5()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new(SlotElement.WILD), new("Вишня"), new("Вишня"), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd2WildsInPositions1And4()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new(SlotElement.WILD), new("Вишня"), new("Вишня"), new(SlotElement.WILD), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd2WildsInPositions1And3()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd2WildsInPositions2And5()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Вишня"), new(SlotElement.WILD), new("Вишня"), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd2WildsInPositions2And4()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Вишня"), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd2WildsInPositions2And3()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd2WildsInPositions3And5()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Вишня"), new("Вишня"), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd2WildsInPositions3And4()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new("Вишня"), new("Вишня"), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd3WildsInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                 new(SlotElement.WILD), new(SlotElement.WILD),new(SlotElement.WILD), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd3WildsInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD),new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd3WildsInCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd3WildsInPositions12And5()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd3WildsInPositions12And4()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new("Вишня"), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd3WildsInPositions15And3()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd3WildsInPositions23And5()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd4WildsInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd4WildsInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd4WildsIntPositions1345()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd4WildsIntPositions1245()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith5ElementsAnd4WildsIntPositions1235()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 5 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd3WildsInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd3WildsInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd3WildsInPositions134()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd3WildsInPositions124()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd3WildsInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd3WildsInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"),  new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd3WildsInPositions245()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd3WildsInPositions235()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd2WildsInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new("Вишня"), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd2WildsInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd2WildsInCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd2WildsInPositions13()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new("Вишня"), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd2WildsInPositions24()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd2WildsInPositions14()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new("Вишня"), new("Вишня"), new(SlotElement.WILD), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd2WildsInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd2WildsInCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd2WildsInPositions25()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"),new(SlotElement.WILD), new("Вишня"), new("Вишня"),  new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd2WildsInPositions24()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd2WildsInPositions35()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Вишня"), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd1WildsInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new("Вишня"), new("Вишня"), new("Вишня"), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd1WildsInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new("Вишня"), new("Вишня"), new(SlotElement.WILD), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd1WildsInPosition2()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new(SlotElement.WILD), new("Вишня"), new("Вишня"), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInStartAnd1WildsInPosition3()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new("Вишня"), new(SlotElement.WILD), new("Вишня"), new("Арбуз")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd1WildsInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new(SlotElement.WILD), new("Вишня"), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd1WildsInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Вишня"), new("Вишня"), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd1WildsInPosition3()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Вишня"), new(SlotElement.WILD), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith4ElementsInEndAnd1WildsInPosition4()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Вишня"), new("Вишня"), new(SlotElement.WILD), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 4 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInStartAnd2WildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new("Арбуз"), new("Виноград"),
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInStartAnd2WildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new("Арбуз"), new("Виноград"),
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInStartAnd2WildInPositions1And3()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new("Вишня"),  new(SlotElement.WILD), new("Арбуз"), new("Виноград"),
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInCenterAnd2WildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня"), new("Виноград"),
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInCenterAnd2WildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD), new("Виноград"),
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInCenterAnd2WildInPositions2And4()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD), new("Виноград"),
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInEndAnd2WildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Виноград"), new(SlotElement.WILD), new(SlotElement.WILD), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInEndAnd2WildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Виноград"), new("Вишня"), new(SlotElement.WILD), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInEndAnd2WildInPositions3And5()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Виноград"), new(SlotElement.WILD), new("Вишня"), new(SlotElement.WILD)
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInStartAndWildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new(SlotElement.WILD), new("Вишня"), new("Вишня"),new("Арбуз"), new("Виноград")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInStartAndWildInCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new(SlotElement.WILD), new("Вишня"), new("Арбуз"), new("Виноград")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInStartAndWildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Вишня"), new("Вишня"), new(SlotElement.WILD), new("Арбуз"), new("Виноград")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInCenterAndWildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new(SlotElement.WILD), new("Вишня"), new("Вишня"), new("Виноград")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInCenterAndWildInCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Вишня"), new(SlotElement.WILD), new("Вишня"), new("Виноград")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInCenterAndWildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Вишня"), new("Вишня"), new(SlotElement.WILD), new("Виноград")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInEndAndWildInStart()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Виноград"), new(SlotElement.WILD), new("Вишня"), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInEndAndWildInCenter()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Виноград"), new("Вишня"), new(SlotElement.WILD), new("Вишня")
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }

    [Test]
    public async Task ComboWhith3ElementsInEndAndWildInEnd()
    {
        //Arrange
        ISlotsCombinationCounterService slotsCombinationCounterService = _serviceProvider.GetRequiredService<ISlotsCombinationCounterService>();
        List<SlotElement> elements =
            [
                  new("Арбуз"), new("Виноград"), new("Вишня"), new("Вишня"), new(SlotElement.WILD),
            ];

        //Act
        SlotsCombo combo = slotsCombinationCounterService.ComboCount(elements);

        //Assert
        if (combo.Combo == 3 && combo.Symbol == "Вишня") Assert.Pass("Комбо собрано");
        else Assert.Fail(combo.Display());
    }
}
