using System.Net.Http.Json;

namespace Gambling.Application.Client.Services.ApiServices;

public static class HttpDataResponce
{
    public static async Task<T?> WithTypeAsync<T> (this HttpResponseMessage response) => await response.Content.ReadFromJsonAsync<T>();
}