using Microsoft.Extensions.DependencyInjection;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Auth;
using Gambling.Application.Core.Abstractions.Balance;
using Gambling.Application.Core.Abstractions.Captcha;
using Gambling.Application.Core.Abstractions.Encryptions;
using Gambling.Application.Core.Abstractions.Game;
using Gambling.Application.Core.Abstractions.Profile;
using Gambling.Application.Core.Abstractions.PromtionCodes;
using Gambling.Application.Core.Abstractions.Sessions;
using Gambling.Application.Core.Abstractions.Statistics;
using Gambling.Application.Core.Abstractions.Token;
using Gambling.Application.Core.Abstractions.Validation;
using Gambling.Application.Core.Services.Encryption;
using Gambling.Application.Core.Services.Token;
using Gambling.Application.Core.Services.Validation;
using Gambling.Application.Client.Services.Account;
using Gambling.Application.Client.Services.Account.Auth;
using Gambling.Application.Client.Services.Account.Auth.Sessions;
using Gambling.Application.Client.Services.Account.Balance;
using Gambling.Application.Client.Services.Account.Balance.PromotionalCodes;
using Gambling.Application.Client.Services.Account.Profile.Statistics;
using Gambling.Application.Client.Services.ApiServices;
using Gambling.Application.Client.Services.Captcha;
using Gambling.Application.Client.Services.Game;
using Gambling.Wpf.Admin.Abstractions;
using Gambling.Wpf.Admin.Services.Navigations;
using Gambling.Wpf.Admin.WelcomeWindowPages;
using Gambling.Wpf.Admin.Windows;
using Gambling.Wpf.Admin.Windows.UserDataTableWindows;

namespace Gambling.Wpf.Admin.Classes;

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

        services.AddTransient<IEmailValidation, EmailValidation>();
        services.AddTransient<ICardValidation, CardValidationService>();
        services.AddTransient<ITwoPasswordsValidation, PasswordValidation>();
        services.AddTransient<IPromocodeValidation, PromocodeValidation>();

        services.AddScoped<ILoginChecker, ClientLoginChecker>();
        services.AddScoped<ISessionStorageService, ClientSessionStorageService>();
        services.AddScoped<IAccountService, ClientAccountService>();
        services.AddScoped<IAccountDataService, ClientAccountDataService>();
        services.AddScoped<IPromotionalCodeService, ClientPromotionalCodeService>();
        services.AddScoped<IUserStatisticsService, ClientUserStatisticService>();

        services.AddScoped<IGameService, ClientGameService>();

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

        services.AddSingleton<DataTablesWindow>();

        services.AddTransient<BalanceChangeWindow>();
        services.AddTransient<PasswordChangeWindow>();
        services.AddTransient<RoleChangeWindow>();
        services.AddTransient<UserBanWindow>();

        services.AddTransient<UserStatisticWindow>();
        services.AddTransient<UserDataWindow>();
        services.AddTransient<PromocodeActivatesViewerWindow>();

        return services;
    }
}