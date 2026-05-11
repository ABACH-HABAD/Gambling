namespace BusinessLogic.ApiServices.Requests;

public record ChangePasswordRequest(string OldHashedPassword, string NewHashedPassword, string RepeatHashedPassword);