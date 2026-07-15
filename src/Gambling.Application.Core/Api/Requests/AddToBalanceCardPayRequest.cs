using Gambling.Application.Core.BusinessModels;

namespace Gambling.Application.Core.Api.Requests;

public record AddToBalanceCardPayRequest(int UserId, PayCard Card, double Count, string? Promocode) : CardPayRequest(UserId, Card);