using BusinessLogic.Account.Balance;

namespace BusinessLogic.ApiServices.Requests;

public abstract record CardPayRequest(PayCard Card) : BaseRequest();