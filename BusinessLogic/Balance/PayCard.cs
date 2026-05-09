namespace BusinessLogic.Balance;

public sealed record class PayCard(string? CardNumber, string? Year, string? Month, string? Owner, string? CvvCvc);