using Gambling.Core.Models;
using Gambling.Infrastructure.Data.Entities;

namespace Gambling.Infrastructure.Data.Projections;

internal static class QueryExtensions
{
    public static IQueryable<UserModel> ToUserModel(this IQueryable<UserEntity> query)
    {
        return query.Select(EntityProjections.ToUserModel);
    }

    public static IQueryable<GameModel> ToGameModel(this IQueryable<GameEntity> query)
    {
        return query.Select(EntityProjections.ToGameModel);
    }

    public static IQueryable<SessionModel> ToSessionModel(this IQueryable<SessionEntity> query)
    {
        return query.Select(EntityProjections.ToSessionModel);
    }

    public static IQueryable<PromotionalCodeModel> ToPromotionalCodeModel(this IQueryable<PromotionalCodeEntity> query)
    {
        return query.Select(EntityProjections.ToPromotionalCodeModel);
    }

    public static IQueryable<PromotionalCodesActivateModel> ToPromotionalCodesActivateModel(this IQueryable<PromotionalCodesActivateEntity> query)
    {
        return query.Select(EntityProjections.ToCodeActivateModel);
    }

    public static IQueryable<BlackjackGameModel> ToBlackjackGameModel(this IQueryable<BlackjackGameEntity> query)
    {
        return query.Select(EntityProjections.ToBlackjackGameModel);
    }
}