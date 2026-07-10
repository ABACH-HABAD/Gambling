using Gambling.Core.Models;

namespace Gambling.Infrastructure.Data.Entities;

internal class PromotionalCodeEntity : BaseEntity<PromotionalCodeEntity, PromotionalCodeModel>
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

    public List<PromotionalCodesActivateEntity> Activates { get; set; } = [];

    internal override PromotionalCodeModel ToModel()
    {
        PromotionalCodeModel dto = new()
        {
            Id = Id,
            Code = Code,
            Use = Use,
            InterestBonus = InterestBonus,
            QuantitativeBonus = QuantitativeBonus,
            FreeSpins = FreeSpins,
        };

        List<PromotionalCodesActivateModel> activatesModels = [];
        foreach (PromotionalCodesActivateEntity activate in Activates)
        {
            activatesModels.Add(activate.ToModel());
        }
        dto.Activates = activatesModels;

        return dto;
    }
}