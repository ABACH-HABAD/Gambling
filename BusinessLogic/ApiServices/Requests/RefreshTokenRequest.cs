namespace BusinessLogic.ApiServices.Requests;

public record RefreshTokenRequest(string Token, int DeviceType) : BaseRequest();
