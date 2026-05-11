using Microsoft.EntityFrameworkCore;
using DataBaseClasses;
using DataBaseClasses.Exceptions;
using DataBaseClasses.Repository;
using DataBaseClasses.Repository.Interfaces;
using BusinessLogic.Game.Blackjack;
using BusinessLogic.Game.Roulette;
using BusinessLogic.Game.Slots;
using BusinessLogic.Validation;
using BusinessLogic.Game;
using BusinessLogic.Account.Auth;
using BusinessLogic.Account.Balance;
using BusinessLogic.Account.Profile.Statistics;
using BusinessLogic.Account;

namespace GamblingWebApi;

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
        services.AddScoped<IGameTypesRepository, GameTypesRepository>();
        services.AddScoped<IDeviceTypeRepository, DeviceTypesRepository>();
        services.AddScoped<IUserStatusesRepository, UserStatusesRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ICardPayService, ServerBalanceService>();
        services.AddScoped<IBalanceService, ServerBalanceService>();

        services.AddScoped<IGameService, ServerGameService>();

        services.AddScoped<ISlotsWinCounterService, SlotsWinCounterService>();
        services.AddScoped<ISlotsCombinationCounterService, SlotsCombinationCounterService>();
        services.AddScoped<ISlotsService, ServerSlotsService>();

        services.AddScoped<IBlackjackService, ServerBlackjackService>();

        services.AddScoped<IRouletteService, ServerRouletteService>();
        services.AddScoped<IRouletteWinCounterService, ServerRouletteWinCounterService>();

        services.AddTransient<ICardValidation, CardValidationService>();
        services.AddTransient<IEmailValidation, EmailValidation>();
        services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();

        services.AddScoped<IAccountService, ServerAccountService>();
        services.AddScoped<IAccountDataService, ServerAccountDataService>();
        services.AddScoped<ISessionService, ServerSessionService>();
        services.AddScoped<ILoginChecker, ServerSessionService>();

        services.AddScoped<IUserStatisticsService, ServerUserStatisticsService>();
        services.AddScoped<IStatisticsService, StatisticsService>();

        return services;
    }
}