using Microsoft.Extensions.DependencyInjection;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Balance;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Application.Core.Abstractions.Encryptions;
using Gambling.Application.Core.Abstractions.Game.Blackjack;
using Gambling.Application.Core.Abstractions.Game.Roulette;
using Gambling.Application.Core.Abstractions.Game.Slots;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Abstractions.Statistics;
using Gambling.Application.Core.Abstractions.Token;
using Gambling.Application.Core.Abstractions.Validation;
using Gambling.Application.Core.Services.Encryption;
using Gambling.Application.Core.Services.Token;
using Gambling.Application.Core.Services.Validation;
using Gambling.Application.Core.Services.Game.Slots;
using Gambling.Application.Core.Services.Game.Blackjack;
using Gambling.Application.Client.Services.Account;
using Gambling.Application.Client.Services.Account.Auth;
using Gambling.Application.Client.Services.Account.Balance;
using Gambling.Application.Client.Services.Account.Profile.Statistics;
using Gambling.Application.Client.Services.ApiServices;
using Gambling.Application.Client.Services.Captcha;
using Gambling.Application.Client.Services.Game.Slots;
using Gambling.Application.Client.Services.Game.Roulette;
using Gambling.Application.Client.Services.Game.Blackjack;
using Gambling.Wpf.User.Abstractions;
using Gambling.Wpf.User.Pages;
using Gambling.Wpf.User.Pages.Games;
using Gambling.Wpf.User.Windows;
using Gambling.Wpf.User.Services.Navigation;

namespace Gambling.Wpf.User.Classes;

internal static class DependencyInjection
{
    internal static IServiceCollection AddBuisnessLogic(this IServiceCollection services)
    {
        services.AddSingleton<IApiClient, ApiClient>();
        services.AddKeyedSingleton<ITokenStorageService, AccessTokenStorageService>("access");
        services.AddKeyedSingleton<ITokenStorageService, RefreshTokenStorageService>("refresh");

        services.AddTransient<IPasswordHasher, Encryption>();
        services.AddTransient<IEncryption, Encryption>();

        services.AddScoped<ICardPayService, ClientBalanceService>();

        services.AddTransient<INameValidation, NameValidationService>();
        services.AddTransient<IEmailValidation, EmailValidation>();
        services.AddTransient<ICardValidation, CardValidationService>();
        services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();

        services.AddScoped<ILoginChecker, ClientLoginChecker>();
        services.AddScoped<IAccountService, ClientAccountService>();
        services.AddScoped<IAccountDataService, ClientAccountDataService>();

        services.AddScoped<ISlotsCombinationCounterService, SlotsCombinationCounterService>();
        services.AddScoped<ISlotsWinCounterService, SlotsWinCounterService>();
        services.AddScoped<ISlotsService, ClientSlotsService>();

        services.AddScoped<IRouletteService, ClientRouletteService>();

        services.AddScoped<IBlackjackService, ClientBlackjackService>();
        services.AddScoped<IBlackjackScoresService, BlackjackScoresService>();

        services.AddTransient<IUserStatisticsService, ClientUserStatisticService>();

        services.AddKeyedScoped<ICaptchaService, EasyCaptchaService>("easy");
        services.AddKeyedScoped<ICaptchaService, SimpleCaptchaService>("simple");

        services.AddSingleton<INavigationService, NavigationService>();

        return services;
    }

    internal static IServiceCollection AddWindowsAndPages(this IServiceCollection services)
    {
        services.AddSingleton<AuthWindow>();

        services.AddSingleton<AuthPage>();
        services.AddSingleton<RegistrationPage>();
        services.AddSingleton<GamesPage>();
        services.AddSingleton<SlotsPage>();
        services.AddSingleton<RoulettePage>();
        services.AddSingleton<BlackjackPage>();
        services.AddSingleton<ProfilePage>();

        services.AddTransient<PayWindow>();
        services.AddTransient<EmailChangeWindow>();
        services.AddTransient<PasswordChangeWindow>();

        return services;
    }
}