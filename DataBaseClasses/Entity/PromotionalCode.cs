namespace DataBaseClasses.Entity;

public class PromotionalCode : Entity
{
    public string? Code { get; set; }
    public int? Use { get; set; }
    public int? InterestBonus { get; set; }
    public int? QuantitativeBonus { get; set; }
}
