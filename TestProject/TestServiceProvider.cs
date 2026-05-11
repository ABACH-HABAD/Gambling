using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataBaseClasses;
using DataBaseClasses.Repository;
using DataBaseClasses.Repository.Interfaces;
using BusinessLogic.ApiServices;
using BusinessLogic.Game.Blackjack;
using BusinessLogic.Game.Roulette;
using BusinessLogic.Game.Slots;
using BusinessLogic.Token;
using BusinessLogic.Encryption;
using BusinessLogic.Validation;
using BusinessLogic.Account.Auth;
using BusinessLogic.Account.Balance;
using BusinessLogic.Account.Profile.Statistics;

namespace TestProject;

public class TestServiceProvider
{
    private static readonly string dataBaseConnectionString = "Server=localhost;Port=3306;User=root;Password=123456789;Database=gambling_testing";

    public static IServiceProvider ProvideServer()
    {
        ServiceCollection services = new();

        services.AddDbContext<ApplicationContext>();

        services.AddSingleton(new DataBaseConnectionString(dataBaseConnectionString));
        services.AddDbContext<ApplicationContext>(options => options.UseMySQL(dataBaseConnectionString));

        services.AddSingleton(new JwtSettings(key: "TestMethodVetySuperSecretJwtKey123456789", issuer: "TestMethod", audience: "TestMethod", expiryMinutes: 0.01));
        services.AddKeyedSingleton<ITokenStorageService, AccessTokenStorageService>("access");
        services.AddKeyedSingleton<ITokenStorageService, RefreshTokenStorageService>("refresh");
        services.AddSingleton<IApiClient, ApiClient>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddScoped<IPromotionalCodesRepository, PromotionalCodesRepository>();
        services.AddScoped<IGameTypesRepository, GameTypesRepository>();
        services.AddScoped<IDeviceTypeRepository, DeviceTypesRepository>();
        services.AddScoped<IUserStatusesRepository, UserStatusesRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IGameRepository, GameRepository>();

        services.AddScoped<ISlotsWinCounterService, SlotsWinCounterService>();
        services.AddScoped<ISlotsCombinationCounterService, SlotsCombinationCounterService>();
        services.AddScoped<ISlotsService, ServerSlotsService>();

        services.AddScoped<IBlackjackService, ServerBlackjackService>();

        services.AddScoped<IRouletteService, ServerRouletteService>();
        services.AddScoped<IRouletteWinCounterService, ServerRouletteWinCounterService>();

        services.AddTransient<ICardValidation, CardValidationService>();
        services.AddTransient<IEmailValidation, EmailValidation>();
        services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();

        services.AddScoped<IBalanceService, ServerBalanceService>();
        services.AddScoped<ICardPayService, ServerBalanceService>();
        services.AddScoped<IAccountService, ServerAccountService>();
        services.AddScoped<ISessionService, ServerSessionService>();
        services.AddScoped<ILoginChecker, ServerSessionService>();

        services.AddTransient<IUserStatisticsService, ServerUserStatisticsService>();
        services.AddTransient<IStatisticsService, StatisticsService>();

        return services.BuildServiceProvider();
    }

    public static IServiceProvider ProvideClient()
    {
        ServiceCollection services = new();

        services.AddKeyedSingleton<ITokenStorageService, AccessTokenStorageService>("access");
        services.AddKeyedSingleton<ITokenStorageService, RefreshTokenStorageService>("refresh");
        services.AddSingleton<IApiClient, TestApiCilent>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IEncryption, Encryption>();
        services.AddScoped<IPasswordHasher, Encryption>();
        services.AddScoped<IUserIdExtraction, Encryption>();

        services.AddScoped<ISlotsWinCounterService, SlotsWinCounterService>();
        services.AddScoped<ISlotsCombinationCounterService, SlotsCombinationCounterService>();
        services.AddScoped<ISlotsService, ClientSlotsService>();

        services.AddScoped<IRouletteService, ClientRouletteService>();

        services.AddTransient<ICardValidation, CardValidationService>();
        services.AddTransient<IEmailValidation, EmailValidation>();
        services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();

        services.AddScoped<ICardPayService, ClientBalanceService>();
        services.AddScoped<IAccountService, ClientAccountService>();
        services.AddScoped<ILoginChecker, ClientLoginChecker>();

        return services.BuildServiceProvider();
    }
}
