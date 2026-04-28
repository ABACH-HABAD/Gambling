namespace BusinessLogic.ApiServices.Requests;

public record RegistrateRequest(string Login, string Password, string RepeatPassword, int DeviceType) : BaseRequest();