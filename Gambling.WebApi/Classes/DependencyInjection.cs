using Microsoft.EntityFrameworkCore;
using Gambling.Core.Abstractions.Repositories;
using Gambling.Core.Exceptions;
using Gambling.Infrastructure.Data;
using Gambling.Infrastructure.Data.Repositories;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Abstractions.PromtionCodes;
using Gambling.Application.Core.Abstractions.Balance;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Sessions;
using Gambling.Application.Core.Abstractions.Game;
using Gambling.Application.Core.Abstractions.Statistics;
using Gambling.Application.Core.Abstractions.Game.Slots;
using Gambling.Application.Core.Abstractions.Game.Roulette;
using Gambling.Application.Core.Abstractions.Game.Blackjack;
using Gambling.Application.Core.Services.Validation;
using Gambling.Application.Core.Services.Statistics;
using Gambling.Application.Core.Services.Game.Blackjack;
using Gambling.Application.Core.Services.Game.Slots;

using Gambling.Application.Core.Abstractions.Validation;
using Gambling.Application.Server.Services.Account.Balance;
using Gambling.Application.Server.Services.Account;
using Gambling.Application.Server.Services.Game;
using Gambling.Application.Server.Services.Account.Balance.PromotionalCodes;
using Gambling.Application.Server.Services.Game.Roulette;
using Gambling.Application.Server.Services.Game.Blackjack;
using Gambling.Application.Server.Services.Account.Profile.Statistics;
using Gambling.Application.Server.Services.Account.Auth;
using Gambling.Application.Server.Services.Game.Slots;
using Gambling.Application.Server.Services.Account.Auth.Sessions;

namespace Gambling.WebApi.Classes;

public static class DependencyInjection
{
    public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration config)
    {
        string dataBaseConnectionString = config.GetConnectionString("DefaultConnection") ?? throw new CannotFindConnectionString();

        services.AddSingleton(new DataBaseConnectionString(dataBaseConnectionString));
        services.AddDbContext<ApplicationContext>(options => options.UseMySQL(dataBaseConnectionString));

        return services;
    }

    public static IServiceCollection AddDataBaseServices(this IServiceCollection services)
    {
        services.AddScoped<IPromotionalCodesRepository, PromotionalCodesRepository>();
        services.AddScoped<IPromotionalCodesActivatesRepository, PromotionalCodesActivatesRepository>();
        services.AddScoped<IGameTypesRepository, GameTypesRepository>();
        services.AddScoped<IDeviceTypeRepository, DeviceTypesRepository>();
        services.AddScoped<IUserStatusesRepository, UserStatusesRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IBlackjackCardRepository, BlackjackCardRepository>();
        services.AddScoped<IBlackjackGameRepository, BlackjackGameRepository>();

        return services;
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ICardPayService, ServerBalanceService>();
        services.AddScoped<IBalanceService, ServerBalanceService>();
        services.AddScoped<IPromotionalCodeService, ServerPromotionalCodeService>();
        services.AddScoped<IPromotionalCodeActivatorService, ServerPromotionalCodeService>();

        services.AddScoped<IGameService, ServerGameService>();

        services.AddScoped<ISlotsWinCounterService, SlotsWinCounterService>();
        services.AddScoped<ISlotsCombinationCounterService, SlotsCombinationCounterService>();
        services.AddScoped<ISlotsService, ServerSlotsService>();

        services.AddScoped<IBlackjackService, ServerBlackjackService>();
        services.AddScoped<IBlackjackGameService, BlackjackGameService>();
        services.AddScoped<IBlackjackScoresService, BlackjackScoresService>();

        services.AddScoped<IRouletteService, ServerRouletteService>();
        services.AddScoped<IRouletteWinCounterService, ServerRouletteWinCounterService>();

        services.AddTransient<ICardValidation, CardValidationService>();
        services.AddTransient<IEmailValidation, EmailValidation>();
        services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();
        services.AddTransient<IPromocodeValidation, PromocodeValidation>();

        services.AddScoped<IAccountService, ServerAccountService>();
        services.AddScoped<IAccountDataService, ServerAccountDataService>();
        services.AddScoped<ISessionService, ServerSessionService>();
        services.AddScoped<ISessionStorageService, ServerSessionService>();
        services.AddScoped<ILoginChecker, ServerSessionService>();

        services.AddScoped<IUserStatisticsService, ServerUserStatisticsService>();
        services.AddScoped<IStatisticsService, StatisticsService>();

        return services;
    }

    public static IServiceCollection AddBackendService(this IServiceCollection services)
    {
        
        services.AddScoped<IIdCheckerService, UserIdCheckerService>();

        return services;
    }
}