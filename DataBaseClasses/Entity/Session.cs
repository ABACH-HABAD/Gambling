namespace DataBaseClasses.Entity;

public class Session : Entity
{
    public int? User { get; set; }
    public DateTime? Time { get; set; }
    public int DeviceType { get; set; }
    public string? Ip { get; set; }
    public string? RefreshToken { get; set; }
    public bool? IsComplete { get; set; }
}