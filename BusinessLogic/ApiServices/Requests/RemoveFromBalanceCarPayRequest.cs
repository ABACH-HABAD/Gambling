using BusinessLogic.Balance;

namespace BusinessLogic.ApiServices.Requests;

public record RemoveFromBalanceCarPayRequest(PayCard Card, double Count) : CardPayRequest(Card);
