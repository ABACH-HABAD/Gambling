namespace Gambling.Core.Models;

public class PromotionalCodeModel : BaseModel
{
    public string Code { get; set; } = string.Empty;
    /// <summary>
    /// Количество использованний
    /// </summary>
    public int Use { get; set; }
    /// <summary>
    /// Процентный бонус
    /// </summary>
    public int InterestBonus { get; set; }

    /// <summary>
    /// Количественный бонус
    /// </summary>
    public int QuantitativeBonus { get; set; }

    /// <summary>
    /// Бонусные игры
    /// </summary>
    public int FreeSpins { get; set; }

    public List<PromotionalCodesActivateModel> Activates { get; set; } = [];
}