using BusinessLogic.ApiServices.Requests;
using BusinessLogic.Token;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BusinessLogic.ApiServices;

public class ApiClient : IApiClient
{
    private readonly ITokenStorageService _accessTokenStorageService;
    private readonly ITokenStorageService _refreshTokenStorageService;
    private readonly HttpClient _httpClient;
    private string? _jwtToken;

    public ApiClient(
        ApiSettings apiSettings,
        [FromKeyedServices("access")] ITokenStorageService accessTokenStorageService,
        [FromKeyedServices("refresh")] ITokenStorageService refreshTokenStorageService)
    {
        _accessTokenStorageService = accessTokenStorageService;
        _refreshTokenStorageService = refreshTokenStorageService;
        _httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(apiSettings.Timeout),
            BaseAddress = new Uri(apiSettings.Url)
        };
    }

    public async Task LoadTokenAsync()
    {
        _jwtToken ??= await _accessTokenStorageService.GetTokenAsync();
        if (_jwtToken != null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
        }
    }

    public async Task<bool> SetTokenAsync(RefreshedTokens? token)
    {
        if (token != null)
        {
            if (token.AccessToken != null && token.AccessToken != string.Empty)
            {
                _jwtToken = token.AccessToken;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
                await _accessTokenStorageService.SaveTokenAsync(token.AccessToken);


            }
            if (token.RefreshToken != null && token.RefreshToken != string.Empty)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
                await _refreshTokenStorageService.SaveTokenAsync(token.RefreshToken);
            }

            return true;
        }
        else
        {
            _jwtToken = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
            await _accessTokenStorageService.ClearTokenAsync();

            return false;
        }
    }

    public async Task<HttpResponseMessage> PostAsync(string endpoint, object? data)
    {
        HttpContent? content = data != null ? JsonContent.Create(data) : null;
        return await _httpClient.PostAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        return await _httpClient.GetAsync(endpoint);
    }

    public async Task<HttpResponseMessage> PutAsync(string endpoint, object? data)
    {
        HttpContent? content = data != null ? JsonContent.Create(data) : null;
        return await _httpClient.PutAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        return await _httpClient.DeleteAsync(endpoint);
    }
}
