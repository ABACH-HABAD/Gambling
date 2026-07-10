using Microsoft.Extensions.DependencyInjection;
using Gambling.Test;
using Gambling.Core.Models;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Api.Results;
using Gambling.Application.Core;

namespace Gambling.Test.AccountTests;

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
            .RegistrateAsync(login, password, repeatPassword, DeviceType.Windows);

        UserModel? userData = await accountDataService.GetUserDataAsync(0);

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
            .RegistrateAsync(login, password, repeatPassword, DeviceType.Windows);

        await accountService.LogoutAsync(string.Empty, DeviceType.Windows, null!);

        //string token = loginResult.Tokens != null ? loginResult.Tokens.RefreshToken : string.Empty;

        UserModel? userData = await accountDataService.GetUserDataAsync(0);

        //Assert
        if (loginResult.Result == true && userData == null) Assert.Pass(loginResult.Message);
        else Assert.Fail(loginResult.Message);
    }
}