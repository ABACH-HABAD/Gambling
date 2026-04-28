namespace BusinessLogic.ApiServices;

public class ApiSettings(string url, int timeout)
{
    public string Url { get; init; } = url;
    public int Timeout { get; init; } = timeout;
}
