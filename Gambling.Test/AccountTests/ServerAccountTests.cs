using Gambling.Application.Core;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Api.Results;
using Gambling.Test;
using Microsoft.Extensions.DependencyInjection;

namespace Gambling.Test.AccountTests;

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
        LoginResult loginResult = await accountService
            .RegistrateAsync(login, password, repeatPassword, DeviceType.Windows);

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
        string expectedMessage = "Введите логин";

        //Act
        LoginResult loginResult = await accountService.RegistrateAsync(login, password, repeatPassword, DeviceType.Windows);

        //Assert
        if (loginResult.Result == false && loginResult.Message == expectedMessage) Assert.Pass(loginResult.Message);
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
        string expectedMessage = "Логин должен быть настоящей электронной почтой";

        //Act
        LoginResult loginResult = await accountService
            .RegistrateAsync(login, password, repeatPassword, DeviceType.Windows);

        //Assert
        if (loginResult.Result == false && loginResult.Message == expectedMessage) 
            Assert.Pass(loginResult.Message);
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
        string expectedMessage = "Пароли не совпадают";

        //Act
        LoginResult loginResult = await accountService.RegistrateAsync(login, password, repeatPassword, DeviceType.Windows);

        //Assert
        if (loginResult.Result == false && loginResult.Message == expectedMessage) Assert.Pass(loginResult.Message);
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
        string expectedMessage = "Аккаунт с таким логином уже существует";

        //Act
        LoginResult FirstAcccountLoginResult = await accountService.RegistrateAsync(login, password, repeatPassword, DeviceType.Windows);
        LoginResult SecondAcccountLoginResult = await accountService.RegistrateAsync(login, password, repeatPassword, DeviceType.Windows);

        //Assert
        if (
            FirstAcccountLoginResult.Result == true && //первый аккаунт зарегестрирован
            SecondAcccountLoginResult.Result == false && //второй нет
            SecondAcccountLoginResult.Message == expectedMessage)
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
        await accountService.RegistrateAsync(login, password, repeatPassword, DeviceType.Windows);
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
        string expectedMessage = "Аккаунт не найден";

        //Act
        LoginResult loginResult = await accountService.LoginAsync(login, password, DeviceType.Windows);

        //Assert
        if (loginResult.Result == false && loginResult.Message == expectedMessage) Assert.Pass(loginResult.Message);
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
        string expectedMessage = "Неверный логин или пароль";

        //Act
        await accountService.RegistrateAsync(login, password, repeatPassword, DeviceType.Windows);
        LoginResult loginResult = await accountService.LoginAsync(login, wrongPassword, DeviceType.Windows);

        //Assert
        if (loginResult.Result == false && loginResult.Message == expectedMessage) Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }
}
