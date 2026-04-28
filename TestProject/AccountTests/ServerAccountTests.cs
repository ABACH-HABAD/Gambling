using BusinessLogic.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject.AccountTests;

public class ServerAccountTests : IntegrationDataBaseTest
{
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await InitializeAsync();
    }

    [Test]
    public async Task RegistrationAccount()
    {
        //Arrange
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        string login = "TestRegistrationAccount@testmail.com";
        string password = "AAA123aaa!";
        string repeatPassword = "AAA123aaa!";

        //Act
        LoginResult loginResult = await accountService.RegistrateAsync(login, password, repeatPassword);

        //Assert
        if (loginResult.Result == true) Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }

    [Test]
    public async Task RegistrationAccountWithoutEmail()
    {
        //Arrange
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        string login = string.Empty;
        string password = "AAA123aaa!";
        string repeatPassword = "AAA123aaa!";

        //Act
        LoginResult loginResult = await accountService.RegistrateAsync(login, password, repeatPassword);

        //Assert
        if (loginResult.Result == false && loginResult.Message == "Введите логин") Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }

    [Test]
    public async Task RegistrationAccountWithWrongEmail()
    {
        //Arrange
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        string login = "ThisIsNotAnEmail";
        string password = "AAA123aaa!";
        string repeatPassword = "AAA123aaa!";

        //Act
        LoginResult loginResult = await accountService.RegistrateAsync(login, password, repeatPassword);

        //Assert
        if (loginResult.Result == false && loginResult.Message == "Логин должен быть настоящей электронной почтой") Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }

    [Test]
    public async Task RegistrationAccountWithMismatchedPasswords()
    {
        //Arrange
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        string login = "AccountWithMismatchedPasswords@testmail.com";
        string password = "AAA123aaa!";
        string repeatPassword = "AnotherPassword";

        //Act
        LoginResult loginResult = await accountService.RegistrateAsync(login, password, repeatPassword);

        //Assert
        if (loginResult.Result == false && loginResult.Message == "Пароли не совпадают") Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }

    [Test]
    public async Task RegistrationTwoAccountsWithSameEmail()
    {
        //Arrange
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        string login = "SameEmail@testmail.com";
        string password = "AAA123aaa!";
        string repeatPassword = "AAA123aaa!";

        //Act
        LoginResult FirstAcccountLoginResult = await accountService.RegistrateAsync(login, password, repeatPassword);
        LoginResult SecondAcccountLoginResult = await accountService.RegistrateAsync(login, password, repeatPassword);

        //Assert
        if (
            FirstAcccountLoginResult.Result == true && //первый аккаунт зарегестрирован
            SecondAcccountLoginResult.Result == false && //второй нет
            SecondAcccountLoginResult.Message == "Аккаунт с таким логином уже существует")
            Assert.Pass(SecondAcccountLoginResult.Message);
        else Assert.Fail(SecondAcccountLoginResult.Message);
    }

    [Test]
    public async Task LoginAccount()
    {
        //Arrange
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        string login = "TestLoginAccount@testmail.com";
        string password = "AAA123aaa!";
        string repeatPassword = "AAA123aaa!";

        //Act
        await accountService.RegistrateAsync(login, password, repeatPassword);
        LoginResult loginResult = await accountService.LoginAsync(login, password, DeviceType.Windows);

        //Assert
        if (loginResult.Result == true) Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }

    [Test]
    public async Task LoginToNonExistentAccount()
    {
        //Arrange
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        string login = "ThisAccountDoesNotExist@testmail.com";
        string password = "AAA123aaa!";

        //Act
        LoginResult loginResult = await accountService.LoginAsync(login, password, DeviceType.Windows);

        //Assert
        if (loginResult.Result == false && loginResult.Message == "Аккаунт не найден") Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }

    [Test]
    public async Task LoginAccountWithWrongPassword()
    {
        //Arrange
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        string login = "WrongPasswordAccount@testmail.com";
        string password = "AAA123aaa!";
        string repeatPassword = "AAA123aaa!";
        string wrongPassword = "WrongPassword";

        //Act
        await accountService.RegistrateAsync(login, password, repeatPassword);
        LoginResult loginResult = await accountService.LoginAsync(login, wrongPassword, DeviceType.Windows);

        //Assert
        if (loginResult.Result == false && loginResult.Message == "Неверный логин или пароль") Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }
}
