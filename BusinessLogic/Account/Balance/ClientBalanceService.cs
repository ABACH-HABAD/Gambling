using BusinessLogic.ApiServices;
using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Game.Slots;
using System.Net.Http.Json;
using System.Net.Sockets;

namespace BusinessLogic.Account.Balance;

public class ClientBalanceService(IApiClient apiClient) : ICardPayService
{
    public async Task<bool> AddToBalanceAsync(int userId, double count, PayCard card, string? promocode = null)
    {
        HttpResponseMessage responseMessage;

        try
        {
            responseMessage = await apiClient.PostAsync("addToBalace/cardPay", new AddToBalanceCardPayRequest(card, count, promocode));
            return responseMessage.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            return false;
        }
    }

    public async Task<bool> RemoveFromBalanceAsync(int userId, double count, PayCard card)
    {
        HttpResponseMessage responseMessage;

        try
        {
            responseMessage = await apiClient.PostAsync("removeFromBalace/cardPay", new RemoveFromBalanceCarPayRequest(card, count));
            return responseMessage.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            return false;
        }
    }
}
