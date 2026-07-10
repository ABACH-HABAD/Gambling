using Microsoft.Extensions.DependencyInjection;
using Gambling.Test;
using Gambling.Core.Models;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Application.Core.Abstractions.Balance;
using Gambling.Application.Core.Abstractions.Game.Slots;
using Gambling.Application.Core.Api.Results;

namespace Gambling.Test.Games.Slots;

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
        UserModel? user = await userRepository.RegistrateAsync(login, password);
        if (user == null)
        {
            Assert.Fail("Аккаунт не зарегистрирован");
            return;
        }
        await balanceService.AddToBalanceAsync(user.Id, balance);
        SlotGameResult? slotGameResult = await slotsService.SpinAsync(user.Id, bet);

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
        UserModel? user = await userRepository.RegistrateAsync(login, password);
        if (user == null)
        {
            Assert.Fail("Аккаунт не зарегистрирован");
            return;
        }
        SlotGameResult? slotGameResult = await slotsService.SpinAsync(user.Id, bet);

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
        UserModel? user = await userRepository.RegistrateAsync(login, password);
        if (user == null)
        {
            Assert.Fail("Аккаунт не зарегистрирован");
            return;
        }
        await userRepository.ChangeSlotsBonusGamesCountAsync(user.Id, bonusGames);
        await balanceService.AddToBalanceAsync(user.Id, balance);

        SlotGameResult? slotGameResult = await slotsService.SpinAsync(user.Id, bet);

        user = await userRepository.GetWithIdAsync(user.Id);

        //Assert
        if (user != null)
        {
            if (slotGameResult != null)
            {
                if (slotGameResult.Result == true) //игра сыграна
                {
                    if (user.Balance >= balance)  //деньги не списаны 
                        Assert.Pass(slotGameResult.Message);
                    else Assert.Fail("Деньги списаны");
                }
                else Assert.Fail(slotGameResult.Message);
            }
            else Assert.Fail("Игра не запущена");
        }
        else Assert.Fail("Аккаунт потерялся");
    }

    [Test]
    public async Task SpinSlotsWithoutAccount()
    {
        //Arrange
        ISlotsService slotsService = _serviceProvider.GetRequiredService<ISlotsService>();
        double bet = 10;

        //Act
        SlotGameResult? slotGameResult = await slotsService.SpinAsync(0, bet);

        //Assert
        if (slotGameResult != null)
        {
            if (slotGameResult.Result == false && slotGameResult.Message == "Аккаунт не найден #0") Assert.Pass(slotGameResult.Message);
            else Assert.Fail(slotGameResult.Message);
        }
        else Assert.Fail("Игра не запущена");
    }
}
