using BusinessLogic.Account;
using BusinessLogic.Account.Auth;
using BusinessLogic.Token;
using DataBaseClasses.Entity;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject.AccountTests;

public class ClientTokenTests : WebApplicationTests
{
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await InitializeAsync();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _factory?.Dispose();
    }

    [Test]
    public async Task RegistrationAccountAndRequireData()
    {
        //Arrange
        IAccountDataService accountDataService = _serviceProvider.GetRequiredService<IAccountDataService>();
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        string login = "TestRegistrationAndTokenAccount@testmail.com";
        string password = "AAA123aaa!";
        string repeatPassword = "AAA123aaa!";

        //Act
        LoginResult loginResult = await accountService
            .RegistrateAsync(login, password, repeatPassword, BusinessLogic.Account.Auth.DeviceType.Windows);

        User? userData = await accountDataService.GetUserDataAsync(0);

        //Assert
        if (loginResult.Result == true && userData != null) Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }

    [Test]
    public async Task RegistrationAccountAndLogout()
    {
        //Arrange
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        IAccountDataService accountDataService = _serviceProvider.GetRequiredService<IAccountDataService>();
        string login = "TestRegistrationAccount@testmail.com";
        string password = "AAA123aaa!";
        string repeatPassword = "AAA123aaa!";

        //Act
        LoginResult loginResult = await accountService
            .RegistrateAsync(login, password, repeatPassword, BusinessLogic.Account.Auth.DeviceType.Windows);

        await accountService.LogoutAsync(string.Empty, BusinessLogic.Account.Auth.DeviceType.Windows, null!);

        //string token = loginResult.Tokens != null ? loginResult.Tokens.RefreshToken : string.Empty;

        User? userData = await accountDataService.GetUserDataAsync(0);

        //Assert
        if (loginResult.Result == true && userData == null) Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }
}
