namespace DataBaseClasses.Entity;

public class User : Entity
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }
    public int Status { get; set; }
    public bool? IsBlocked { get; set; }
    public double? Coefficient { get; set; }
    public double? Balance { get; set; }
    public int? WinCount { get; set; }
    public int? LossCount { get; set; }
    public double? WinBalance { get; set; }
    public double? LossBalance { get; set; }
    public int? SlotsBonusCount { get; set; }
}
