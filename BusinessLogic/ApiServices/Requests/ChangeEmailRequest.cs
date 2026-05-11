namespace BusinessLogic.ApiServices.Requests;

public record ChangeEmailRequest(string OldEmail, string NewEmail) : BaseRequest;