using Gambling.Application.Client.Services.ApiServices;
using Gambling.Application.Core.Abstractions.Api;
using Gambling.Application.Core.Abstractions.Token;
using Gambling.Application.Core.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Gambling.Test;

public class TestApiCilent : IApiClient
{
    private ITokenStorageService? _accessTokenStorageService;
    private ITokenStorageService? _refreshTokenStorageService;
    private HttpClient? _httpClient;
    private string? _jwtToken;

    public async Task InitializeAsync(HttpClient client, ApiSettings apiSettings,
        [FromKeyedServices("access")] ITokenStorageService accessTokenStorageService,
        [FromKeyedServices("refresh")] ITokenStorageService refreshTokenStorageService)
    {
        _accessTokenStorageService = accessTokenStorageService;
        _refreshTokenStorageService = refreshTokenStorageService;

        _httpClient = client;
        _httpClient.Timeout = TimeSpan.FromSeconds(apiSettings.Timeout);
        _httpClient.BaseAddress = new Uri(apiSettings.Url);
    }

    public async Task LoadTokenAsync()
    {
        if (_accessTokenStorageService == null || _refreshTokenStorageService == null || _httpClient == null) throw new Exception("Используй InitializeAsync");

        _jwtToken ??= await _accessTokenStorageService.GetTokenAsync();
        if (_jwtToken != null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
        }
    }

    public async Task<bool> SetTokenAsync(RefreshedTokens? token)
    {
        if (_accessTokenStorageService == null || _refreshTokenStorageService == null || _httpClient == null) throw new Exception("Используй InitializeAsync");

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
        if (_accessTokenStorageService == null || _refreshTokenStorageService == null || _httpClient == null) throw new Exception("Используй InitializeAsync");

        HttpContent? content = data != null ? JsonContent.Create(data) : null;
        return await _httpClient.PostAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        if (_accessTokenStorageService == null || _refreshTokenStorageService == null || _httpClient == null) throw new Exception("Используй InitializeAsync");

        return await _httpClient.GetAsync(endpoint);
    }

    public async Task<HttpResponseMessage> PutAsync(string endpoint, object? data)
    {
        if (_accessTokenStorageService == null || _refreshTokenStorageService == null || _httpClient == null) throw new Exception("Используй InitializeAsync");

        HttpContent? content = data != null ? JsonContent.Create(data) : null;
        return await _httpClient.PutAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        if (_accessTokenStorageService == null || _refreshTokenStorageService == null || _httpClient == null) throw new Exception("Используй InitializeAsync");

        return await _httpClient.DeleteAsync(endpoint);
    }
}
