using System.Linq.Expressions;
using Gambling.Core.Models;
using Gambling.Infrastructure.Data.Entities;

namespace Gambling.Infrastructure.Data.Projections;

internal static class EntityProjections
{
    //User
    public static readonly Expression<Func<UserEntity, UserModel>> ToUserModel = user => new UserModel()
    {
        Id = user.Id,
        Email = user.Email,
        Name = user.Name,
        Balance = user.Balance,
        Coefficient = user.Coefficient,
        IsBlocked = user.IsBlocked,
        LossBalance = user.LossBalance,
        LossCount = user.LossCount,
        WinBalance = user.WinBalance,
        WinCount = user.WinCount,
        SlotsBonusCount = user.SlotsBonusCount,
        Status = user.Status != null ? new UserStatusModel
        {
            Id = user.Status.Id,
            Name = user.Status.Name
        } : null
    };

    //Game
    public static readonly Expression<Func<GameEntity, GameModel>> ToGameModel = game =>
    new GameModel
    {
        Id = game.Id,
        Bet = game.Bet,
        WinAmount = game.WinAmount,
        IsWin = game.IsWin,
        PlayerId = game.PlayerId,
        GameType = game.GameType != null ?
        new GameTypeModel
        {
            Id = game.GameType.Id,
            Name = game.GameType.Name
        }
        : null,
        Player = game.Player != null ?
        new UserModel
        {
            Id = game.Player.Id,
            Email = game.Player.Email,
            Name = game.Player.Name,
            Balance = game.Player.Balance,
            Coefficient = game.Player.Coefficient,
            IsBlocked = game.Player.IsBlocked,
            LossBalance = game.Player.LossBalance,
            LossCount = game.Player.LossCount,
            SlotsBonusCount = game.Player.SlotsBonusCount,
            WinBalance = game.Player.WinBalance,
            WinCount = game.Player.WinCount,
            Status = game.Player.Status != null ?
            new UserStatusModel
            {
                Id = game.Player.Status.Id,
                Name = game.Player.Status.Name
            } : null
        } : null
    };

    //Session
    public static readonly Expression<Func<SessionEntity, SessionModel>> ToSessionModel = session => new SessionModel
    {
        Id = session.Id,
        UserId = session.UserId,
        Ip = session.Ip,
        IsComplete = session.IsComplete,
        Time = session.Time,
        DeviceType = session.DeviceType != null ?
        new DeviceTypeModel
        {
            Id = session.DeviceType.Id,
            Name = session.DeviceType.Name
        } : null,
        User = session.User != null ?
        new UserModel
        {
            Id = session.User.Id,
            Name = session.User.Name,
            Email = session.User.Email,
            Balance = session.User.Balance,
            Coefficient = session.User.Coefficient,
            WinBalance = session.User.WinBalance,
            LossBalance = session.User.LossBalance,
            WinCount = session.User.WinCount,
            LossCount = session.User.LossCount,
            IsBlocked = session.User.IsBlocked,
            SlotsBonusCount = session.User.SlotsBonusCount,
            Status = session.User.Status != null ? new UserStatusModel
            {
                Id = session.User.Status.Id,
                Name = session.User.Status.Name,
            } : null
        } : null
    };

    //Promocode
    public static readonly Expression<Func<PromotionalCodeEntity, PromotionalCodeModel>> ToPromotionalCodeModel = code =>
    new PromotionalCodeModel
    {
        Id = code.Id,
        Code = code.Code,
        Use = code.Use,
        FreeSpins = code.FreeSpins,
        InterestBonus = code.InterestBonus,
        QuantitativeBonus = code.QuantitativeBonus,
        Activates = code.Activates
        .Select(activate => new PromotionalCodesActivateModel
        {
            Id = activate.Id,
            PromotionalCodeId = activate.PromotionalCodeId,
            UserId = activate.UserId,
            User = activate.User != null ? new UserModel
            {
                Id = activate.User.Id,
                Email = activate.User.Email,
                Name = activate.User.Name,
                Balance = activate.User.Balance,
                Coefficient = activate.User.Coefficient,
                IsBlocked = activate.User.IsBlocked,
                LossBalance = activate.User.LossBalance,
                LossCount = activate.User.LossCount,
                WinBalance = activate.User.WinBalance,
                WinCount = activate.User.WinCount,
                SlotsBonusCount = activate.User.SlotsBonusCount,
                Status = activate.User.Status != null ? new UserStatusModel
                {
                    Id = activate.User.Status.Id,
                    Name = activate.User.Status.Name
                } : null
            } : null
        })
        .ToList()
    };

    //PromocodeActivation
    public static readonly Expression<Func<PromotionalCodesActivateEntity, PromotionalCodesActivateModel>> ToCodeActivateModel = activate =>
    new PromotionalCodesActivateModel
    {
        Id = activate.Id,
        PromotionalCodeId = activate.PromotionalCodeId,
        UserId = activate.UserId,
        User = activate.User != null ?
        new UserModel
        {
            Id = activate.User.Id,
            Email = activate.User.Email,
            Name = activate.User.Name,
            Balance = activate.User.Balance,
            Coefficient = activate.User.Coefficient,
            IsBlocked = activate.User.IsBlocked,
            LossBalance = activate.User.LossBalance,
            LossCount = activate.User.LossCount,
            WinBalance = activate.User.WinBalance,
            WinCount = activate.User.WinCount,
            SlotsBonusCount = activate.User.SlotsBonusCount,
            Status = activate.User.Status != null ? new UserStatusModel
            {
                Id = activate.User.Status.Id,
                Name = activate.User.Status.Name
            } : null
        } : null
    };

    //BlackjackGame
    public static readonly Expression<Func<BlackjackGameEntity, BlackjackGameModel>> ToBlackjackGameModel = game =>
    new BlackjackGameModel()
    {
        Id = game.Id,
        Bet = game.Bet,
        CanPlayerMove = game.CanPlayerMove,
        UserId = game.UserId,
        User = game.User != null ?
        new UserModel
        {
            Id = game.User.Id,
            Email = game.User.Email,
            Name = game.User.Name,
            Balance = game.User.Balance,
            IsBlocked = game.User.IsBlocked,
            WinBalance = game.User.WinBalance,
            WinCount = game.User.WinCount,
            LossBalance = game.User.LossBalance,
            LossCount = game.User.LossCount,
            Coefficient = game.User.Coefficient,
            SlotsBonusCount = game.User.SlotsBonusCount,
            Status = game.User.Status != null ?
            new UserStatusModel
            {
                Id = game.User.Status.Id,
                Name = game.User.Status.Name
            } : null
        } : null
    };
}