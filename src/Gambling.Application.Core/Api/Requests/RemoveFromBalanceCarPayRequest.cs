using Gambling.Application.Core.BusinessModels;

namespace Gambling.Application.Core.Api.Requests;

public record RemoveFromBalanceCarPayRequest(int UserId, PayCard Card, double Count) : CardPayRequest(UserId, Card);