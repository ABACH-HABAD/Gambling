using BusinessLogic.Auth;
using BusinessLogic.Auth.Validation;
using BusinessLogic.Game.Blackjack;
using BusinessLogic.Game.Roulette;
using BusinessLogic.Game.Slots;
using BusinessLogic.Profile;
using DataBaseClasses;
using DataBaseClasses.Repository;
using DataBaseClasses.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace TestProject
{
    public class TestServiceProvider
    {
        public static IServiceProvider Provide()
        {
            ServiceCollection services = new();
            services.AddDbContext<ApplicationContext>();

            string dataBaseConnectionString = "Server=localhost;Port=3306;User=root;Password=123456789;Database=gambling_testing";

            services.AddSingleton<string>(dataBaseConnectionString);
            services.AddDbContext<ApplicationContext>(options => options.UseMySQL(dataBaseConnectionString));

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

            services.AddScoped<IRouletteService, ServerRouletterService>();
            services.AddScoped<IRouletteWinCounterService, ServerRouletteWinCounterService>();

            services.AddTransient<IValidation, EmailValidation>();
            services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();
            services.AddScoped<IAccountService, ServerAccountService>();
            services.AddScoped<ISessionService, ServerSessionService>();
            services.AddScoped<ILoginChecker, ServerSessionService>();

            services.AddTransient<IUserStatisticsService, UserStatisticsService>();
            services.AddTransient<IStatisticsService, StatisticsService>();

            return services.BuildServiceProvider();
        }
    }
}
