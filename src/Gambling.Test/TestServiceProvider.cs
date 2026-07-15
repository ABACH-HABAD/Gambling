using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Infrastructure.Data;
using Gambling.Infrastructure.Data.Repositories;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Balance;
using Gambling.Application.Core.Abstractions.Game.Blackjack;
using Gambling.Application.Core.Abstractions.Game.Roulette;
using Gambling.Application.Core.Abstractions.Game.Slots;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Abstractions.PromtionCodes;
using Gambling.Application.Core.Abstractions.Sessions;
using Gambling.Application.Core.Abstractions.Validation;
using Gambling.Application.Core.Abstractions.Encryptions;
using Gambling.Application.Core.Abstractions.Statistics;
using Gambling.Application.Core.Abstractions.Token;
using Gambling.Application.Core.Services.Encryption;
using Gambling.Application.Core.Services.Statistics;
using Gambling.Application.Core.Services.Token;
using Gambling.Application.Core.Services.Validation;
using Gambling.Application.Core.Services.Game.Slots;
using Gambling.Application.Client.Services.Account;
using Gambling.Application.Client.Services.Account.Balance.PromotionalCodes;
using Gambling.Application.Client.Services.ApiServices;
using Gambling.Application.Client.Services.Account.Balance;
using Gambling.Application.Client.Services.Account.Auth;
using Gambling.Application.Client.Services.Game.Slots;
using Gambling.Application.Client.Services.Game.Roulette;
using Gambling.Application.Server.Services.Account.Balance;
using Gambling.Application.Server.Services.Account.Balance.PromotionalCodes;
using Gambling.Application.Server.Services.Game.Roulette;
using Gambling.Application.Server.Services.Game.Blackjack;
using Gambling.Application.Server.Services.Account.Profile.Statistics;
using Gambling.Application.Server.Services.Account.Auth;
using Gambling.Application.Server.Services.Game.Slots;
using Gambling.Application.Server.Services.Account.Auth.Sessions;

namespace Gambling.Test;

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
        services.AddScoped<IPromotionalCodesActivatesRepository, PromotionalCodesActivatesRepository>();
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

        services.AddScoped<IPromotionalCodeActivatorService, ServerPromotionalCodeService>();
        services.AddScoped<IPromotionalCodeService, ServerPromotionalCodeService>();

        services.AddTransient<ICardValidation, CardValidationService>();
        services.AddTransient<IEmailValidation, EmailValidation>();
        services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();
        services.AddTransient<IPromocodeValidation, PromocodeValidation>();

        services.AddScoped<IBalanceService, ServerBalanceService>();
        services.AddScoped<ICardPayService, ServerBalanceService>();
        services.AddScoped<IAccountService, ServerAccountService>();
        services.AddScoped<ISessionService, ServerSessionService>();
        services.AddScoped<ILoginChecker, ServerSessionService>();

        services.AddScoped<IUserStatisticsService, ServerUserStatisticsService>();
        services.AddScoped<IStatisticsService, StatisticsService>();

        return services.BuildServiceProvider();
    }

    public static IServiceProvider ProvideClient()
    {
        ServiceCollection services = new();

        services.AddKeyedSingleton<ITokenStorageService, AccessTokenStorageService>("access");
        services.AddKeyedSingleton<ITokenStorageService, RefreshTokenStorageService>("refresh");
        services.AddSingleton<IApiClient, TestApiCilent>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IEncryption, EncryptionService>();
        services.AddScoped<IPasswordHasher, PasswordHasherService>();

        services.AddScoped<IAccountDataService, ClientAccountDataService>();
        services.AddScoped<IPromotionalCodeService, ClientPromotionalCodeService>();

        services.AddScoped<ISlotsWinCounterService, SlotsWinCounterService>();
        services.AddScoped<ISlotsCombinationCounterService, SlotsCombinationCounterService>();
        services.AddScoped<ISlotsService, ClientSlotsService>();

        services.AddScoped<IRouletteService, ClientRouletteService>();

        services.AddTransient<ICardValidation, CardValidationService>();
        services.AddTransient<IEmailValidation, EmailValidation>();
        services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();
        services.AddTransient<IPromocodeValidation, PromocodeValidation>();

        services.AddScoped<ICardPayService, ClientBalanceService>();
        services.AddScoped<IAccountService, ClientAccountService>();
        services.AddScoped<ILoginChecker, ClientLoginChecker>();

        return services.BuildServiceProvider();
    }
}
