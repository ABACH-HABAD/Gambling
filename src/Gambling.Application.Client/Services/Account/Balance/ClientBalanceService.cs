using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Balance;
using Gambling.Application.Core.Api.Requests;
using Gambling.Application.Core.BusinessModels;

namespace Gambling.Application.Client.Services.Account.Balance;

public class ClientBalanceService(IApiClient apiClient) : ICardPayService
{
    public async Task<bool> AddToBalanceAsync(int userId, double count, PayCard card, string? promocode = null)
    {
        if (count <= 0) throw new ValidationException("Пополнение должно быть больше 0");

        HttpResponseMessage responseMessage;

        try
        {
            responseMessage = await apiClient.PostAsync("addToBalace/cardPay", new AddToBalanceCardPayRequest(userId, card, count, promocode));
            return responseMessage.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            return false;
        }
    }

    public async Task<bool> RemoveFromBalanceAsync(int userId, double count, PayCard card)
    {
        if (count <= 0) throw new ValidationException("Нельзя снять отрицательное количество денег");

        HttpResponseMessage responseMessage;

        try
        {
            responseMessage = await apiClient.PostAsync("removeFromBalace/cardPay", new RemoveFromBalanceCarPayRequest(userId, card, count));
            return responseMessage.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            return false;
        }
    }
}