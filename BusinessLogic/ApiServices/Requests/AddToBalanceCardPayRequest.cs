using BusinessLogic.Balance;

namespace BusinessLogic.ApiServices.Requests;

public record AddToBalanceCardPayRequest(PayCard Card, double Count, string? Promocode) : CardPayRequest(Card);