using BusinessLogic.Auth;
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
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        string login = "TestRegistrationAndTokenAccount@testmail.com";
        string password = "AAA123aaa!";
        string repeatPassword = "AAA123aaa!";

        //Act
        LoginResult loginResult = await accountService
            .RegistrateAsync(login, password, repeatPassword, BusinessLogic.Auth.DeviceType.Windows);

        User? userData = await accountService.GetUserDataAsync(0);

        //Assert
        if (loginResult.Result == true && userData != null) Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }

    [Test]
    public async Task RegistrationAccountAndLogout()
    {
        //Arrange
        IAccountService accountService = _serviceProvider.GetRequiredService<IAccountService>();
        string login = "TestRegistrationAccount@testmail.com";
        string password = "AAA123aaa!";
        string repeatPassword = "AAA123aaa!";

        //Act
        LoginResult loginResult = await accountService
            .RegistrateAsync(login, password, repeatPassword, BusinessLogic.Auth.DeviceType.Windows);

        await accountService.LogoutAsync(string.Empty, BusinessLogic.Auth.DeviceType.Windows, null!);

        //string token = loginResult.Tokens != null ? loginResult.Tokens.RefreshToken : string.Empty;

        User? userData = await accountService.GetUserDataAsync(0);

        //Assert
        if (loginResult.Result == true && userData == null) Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }
}
