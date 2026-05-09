using BusinessLogic.Auth;
using BusinessLogic.Balance;
using BusinessLogic.Game.Slots;
using DataBaseClasses.Entity;
using DataBaseClasses.Repository;
using DataBaseClasses.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject.Games.Slots;

public class ServerSlotsTestsWithAccount : IntegrationDataBaseTest
{
    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        await InitializeAsync();
    }

    [Test]
    public async Task SpinSlots()
    {
        //Arrange
        IUserRepository userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
        ISlotsService slotsService = _serviceProvider.GetRequiredService<ISlotsService>();
        IBalanceService balanceService = _serviceProvider.GetRequiredService<IBalanceService>();
        string login = "SpinSlotsTestAccount@testmail.com";
        string password = "AAA123aaa!";
        double balance = 10;
        double bet = 10;

        //Act
        User? user = userRepository.Registrate(login, password);
        if (user == null)
        {
            Assert.Fail("Аккаунт не зарегистрирован");
            return;
        }
        await balanceService.AddToBalanceAsync(user.Id, balance);
        SlotGameResult? slotGameResult = await slotsService.Spin(user.Id, bet);

        //Assert
        if (slotGameResult != null)
        {
            if (slotGameResult.Result == true) Assert.Pass(slotGameResult.Message);
            else Assert.Fail(slotGameResult.Message);
        }
        else Assert.Fail("Игра не запущена");
    }

    [Test]
    public async Task SpinSlotsWithoutMoney()
    {
        //Arrange
        IUserRepository userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
        ISlotsService slotsService = _serviceProvider.GetRequiredService<ISlotsService>();
        string login = "SpinSlotsWithoutMoney@testmail.com";
        string password = "AAA123aaa!";
        double bet = 10;

        //Act
        User? user = userRepository.Registrate(login, password);
        if (user == null)
        {
            Assert.Fail("Аккаунт не зарегистрирован");
            return;
        }
        SlotGameResult? slotGameResult = await slotsService.Spin(user.Id, bet);

        //Assert
        if (slotGameResult != null)
        {
            if (slotGameResult.Result == false && slotGameResult.Message == "На балансе недостаточно средств") Assert.Pass(slotGameResult.Message);
            else Assert.Fail(slotGameResult.Message);
        }
        else Assert.Fail("Игра не запущена");
    }


    [Test]
    public async Task SpinBonusGame()
    {
        //Arrange
        ISlotsService slotsService = _serviceProvider.GetRequiredService<ISlotsService>();
        IUserRepository userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
        IBalanceService balanceService = _serviceProvider.GetRequiredService<IBalanceService>();
        string login = "SpinSlotsBonusGame@testmail.com";
        string password = "AAA123aaa!";
        double balance = 10;
        double bet = 10;
        int bonusGames = 50;

        //Act
        User? user = userRepository.Registrate(login, password);
        if (user == null)
        {
            Assert.Fail("Аккаунт не зарегистрирован");
            return;
        }
        userRepository.ChangeSlotsBonusGamesCount(user.Id, bonusGames);
        await balanceService.AddToBalanceAsync(user.Id, balance);

        SlotGameResult? slotGameResult = await slotsService.Spin(user.Id, bet);

        //Assert
        if (slotGameResult != null)
        {
            if (slotGameResult.Result == true) //игра сыграна
            {
                if (user.Balance >= balance)  //деньги не списаны 
                    Assert.Pass(slotGameResult.Message); 
                else  Assert.Fail("Деньги списаны");
            }
            else Assert.Fail(slotGameResult.Message);
        }
        else Assert.Fail("Игра не запущена");
    }

    [Test]
    public async Task SpinSlotsWithoutAccount()
    {
        //Arrange
        ISlotsService slotsService = _serviceProvider.GetRequiredService<ISlotsService>();
        double bet = 10;

        //Act
        SlotGameResult? slotGameResult = await slotsService.Spin(0, bet);

        //Assert
        if (slotGameResult != null)
        {
            if (slotGameResult.Result == false && slotGameResult.Message == "Аккаунт не найден #0") Assert.Pass(slotGameResult.Message);
            else Assert.Fail(slotGameResult.Message);
        }
        else Assert.Fail("Игра не запущена");
    }
}
