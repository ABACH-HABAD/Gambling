namespace BusinessLogic.Account.Profile.Statistics;

public record UserStatisticResult(double WinFrequency, int WinCount, int LossCount, int TotalCount, double WinBalance, double LossBalance, double TotalBalance);