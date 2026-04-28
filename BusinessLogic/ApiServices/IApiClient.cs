using BusinessLogic.Token;

namespace BusinessLogic.ApiServices;

public interface IApiClient
{
    public Task LoadTokenAsync();
    public Task<bool> SetTokenAsync(RefreshedTokens? token);

    public Task<HttpResponseMessage> GetAsync(string endpoint);
    public Task<HttpResponseMessage> PostAsync(string endpoint, object? data);
    public Task<HttpResponseMessage> PutAsync(string endpoint, object? data);
    public Task<HttpResponseMessage> DeleteAsync(string endpoint);
}
