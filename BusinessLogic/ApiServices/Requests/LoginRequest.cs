namespace BusinessLogic.ApiServices.Requests;

public record LoginRequest(string Login, string Password, int DeviceType) : BaseRequest();
