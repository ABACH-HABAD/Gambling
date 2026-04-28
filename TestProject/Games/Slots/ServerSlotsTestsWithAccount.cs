using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.Auth;
using BusinessLogic.Game.Slots;
using DataBaseClasses.Repository.Interfaces;
using DataBaseClasses.Entity;
/*
namespace TestProject.Games.Slots
{
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
            ISlotsService slotsService = _serviceProvider.GetRequiredService<ISlotsService>();
            IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
            string login = "SpinSlotsTestAccount@testmail.com";
            string password = "AAA123aaa!";
            string repeatPassword = "AAA123aaa!";
            double balance = 10;
            double bet = 10;

            //Act
            LoginResult loginResult = await accountService.RegistrateAsync(login, password, repeatPassword);

            await accountService.TopUpBalance(loginResult.UserId, balance);
            SlotGameResult? slotGameResult = await slotsService.Spin(loginResult.UserId, bet);

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
            ISlotsService slotsService = _serviceProvider.GetRequiredService<ISlotsService>();
            IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
            string login = "SpinSlotsWithoutMoney@testmail.com";
            string password = "AAA123aaa!";
            string repeatPassword = "AAA123aaa!";
            double bet = 10;

            //Act
            LoginResult loginResult = await accountService.RegistrateAsync(login, password, repeatPassword);
            SlotGameResult? slotGameResult = await slotsService.Spin(loginResult.UserId, bet);

            //Assert
            if (slotGameResult != null && slotGameResult.Message == "На балансе недостаточно средств")
            {
                if (slotGameResult.Result == false) Assert.Pass(slotGameResult.Message);
                else Assert.Fail(slotGameResult.Message);
            }
            else Assert.Fail("Игра не запущена");
        }


        [Test]
        public async Task SpinBonusGame()
        {
            //Arrange
            ISlotsService slotsService = _serviceProvider.GetRequiredService<ISlotsService>();
            IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
            IUserRepository userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
            string login = "SpinSlotsBonusGame@testmail.com";
            string password = "AAA123aaa!";
            string repeatPassword = "AAA123aaa!";
            double balance = 10;
            double bet = 10;
            int bonusGames = 50;

            //Act
            LoginResult loginResult = await accountService.RegistrateAsync(login, password, repeatPassword);
            userRepository.ChangeSlotsBonusGamesCount(loginResult.UserId, bonusGames);
            await accountService.TopUpBalance(loginResult.UserId, balance);

            SlotGameResult? slotGameResult = await slotsService.Spin(loginResult.UserId, bet);

            //Assert
            if (slotGameResult != null)
            {
                User? user = userRepository.GetWithId(loginResult.UserId);
                if (user != null)
                {
                    if (slotGameResult.Result == true) //игра сыграна
                    {
                        if (user.Balance >= balance) //деньги не списаны
                        {
                            Assert.Pass(slotGameResult.Message);
                        }
                        else
                        {
                            Assert.Fail("Деньги списаны");
                        }
                    }
                    else Assert.Fail(slotGameResult.Message);
                }
                else Assert.Fail("Аккаунт не найден");
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
}
*/