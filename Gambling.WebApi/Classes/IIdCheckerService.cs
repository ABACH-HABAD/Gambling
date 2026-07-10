using System.Diagnostics.CodeAnalysis;
using Gambling.Application.Core.Api.Requests;
using Gambling.Core.Models;

namespace Gambling.WebApi.Classes;

public interface IIdCheckerService
{
    public bool GetUserIdFormContext(HttpContext httpContext, [MaybeNullWhen(false)] out int userId, [MaybeNullWhen(true)] out IResult result);

    /// <summary>
    /// Пример использования:
    /// 1) запрашиваем службу IIdChecker idChecker
    /// 2) используем для получения userId в GET методах. Если пользователь пытается получить чужие данные, то возвращается ошибка 403
    /// if (!idChecker.CheckUserId(httpContext, ref userId, out IResult? httpResult)) return httpResult;
    /// 3) используем в POST PUT PATCH методах.
    /// if (!idChecker.CheckUserId(httpContext, userData, out int userId, out IResult? httpResult)) return httpResult;
    /// </summary>
    /// <param name="httpContext">контекст запроса</param>
    /// <param name="userId">пришедший в GET запросе id</param>
    /// <param name="result">HTTP код (возвращается только при ошибке</param>
    /// <returns>true когда id действительный</returns>
    public bool CheckUserId(HttpContext httpContext, ref int userId, [MaybeNullWhen(true)] out IResult result);

    /// <summary>
    /// Пример использования:
    /// 1) запрашиваем службу IIdChecker idChecker
    /// 2) используем для получения userId в POST PUT PATCH методах. Если пользователь пытается изменить чужие данные, то возвращается ошибка 403
    /// if (!idChecker.CheckUserId(httpContext, request, out int userId, out IResult? httpResult)) return httpResult;
    /// </summary>
    /// <param name="httpContext">контекст запроса</param>
    /// <param name="userModel">пришедшие в запросе данные</param>
    /// <param name="userId">найденный id</param>
    /// <param name="result">HTTP код (возвращается только при ошибке</param>
    /// <returns>true когда id действительный</returns>
    public bool GetUserIdFormRequest(HttpContext httpContext, BaseRequiringIdRequest request, [MaybeNullWhen(false)] out int userId, [MaybeNullWhen(true)] out IResult result);

    /// <summary>
    /// Пример использования:
    /// 1) запрашиваем службу IIdChecker idChecker
    /// 2) используем для получения userId в POST PUT PATCH методах. Если пользователь пытается изменить чужие данные, то возвращается ошибка 403
    /// if (!idChecker.CheckUserId(httpContext, userData, out int userId, out IResult? httpResult)) return httpResult;
    /// </summary>
    /// <param name="httpContext">контекст запроса</param>
    /// <param name="userModel">пришедшие в запросе данные пользователя</param>
    /// <param name="userId">найденный id</param>
    /// <param name="result">HTTP код (возвращается только при ошибке</param>
    /// <returns>true когда id действительный</returns>
    public bool GetUserIdFormUserModel(HttpContext httpContext, UserModel userModel, [MaybeNullWhen(false)] out int userId, [MaybeNullWhen(true)] out IResult result);
}