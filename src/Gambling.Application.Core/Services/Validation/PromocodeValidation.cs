using Gambling.Application.Core.Abstractions.Validation;

namespace Gambling.Application.Core.Services.Validation;

public class PromocodeValidation : IPromocodeValidation
{
    public bool Validate(string code, string uses, string interestBonus, string quantitativeBonus, string freeSpins, out string error)
    {
        //Код
        if (code == null || code == string.Empty)
        {
            error = "Промокод не должен быть пустой";
            return false;
        }

        //Количество использований
        if (uses == null || uses == string.Empty)
        {
            error = "Количество использованний не должен быть пустой";
            return false;
        }
        else if (!int.TryParse(uses, out int count))
        {
            error = "Количество использованний должно быть целым числом";
            return false;
        }
        else if (count <= 0)
        {
            error = "Количество использованний должно быть больше нуля";
            return false;
        }

        //Процентный бонус
        if (interestBonus == null || interestBonus == string.Empty)
        {
            error = "Процентный бонус не должен быть пустой";
            return false;
        }
        else if (!int.TryParse(interestBonus, out int count))
        {
            error = "Процентный бонус должно быть целым числом";
            return false;
        }
        else if (count <= 0)
        {
            error = "Процентный бонус должно быть больше нуля";
            return false;
        }

        //Количественный бонус
        if (quantitativeBonus == null || quantitativeBonus == string.Empty)
        {
            error = "Количественный бонус не должен быть пустой";
            return false;
        }
        else if (!int.TryParse(quantitativeBonus, out int count))
        {
            error = "Количественный бонус должно быть целым числом";
            return false;
        }
        else if (count <= 0)
        {
            error = "Количественный бонус должно быть больше нуля";
            return false;
        }

        //Бонусные игры
        if (freeSpins == null || freeSpins == string.Empty)
        {
            error = "Количество бонусных игр не должено быть пустым";
            return false;
        }
        else if (!int.TryParse(freeSpins, out int count))
        {
            error = "Количество бонусных игр должно быть целым числом";
            return false;
        }
        else if (count <= 0)
        {
            error = "Количество бонусных игр должно быть больше нуля";
            return false;
        }

        error = string.Empty;
        return true;
    }

    public bool Validate(string code, int uses, int interestBonus, int quantitativeBonus, int freeSpins, out string error)
    {
        //Код
        if (code == null || code == string.Empty)
        {
            error = "Промокод не должен быть пустой";
            return false;
        }

        //Количество использований
        else if (uses <= 0)
        {
            error = "Количество использованний должно быть больше нуля";
            return false;
        }

        //Процентный бонус
        else if (interestBonus <= 0)
        {
            error = "Процентный бонус должно быть больше нуля";
            return false;
        }

        //Количественный бонус
        else if (quantitativeBonus <= 0)
        {
            error = "Количественный бонус должно быть больше нуля";
            return false;
        }

        //Бонусные игры
        else if (freeSpins <= 0)
        {
            error = "Количество бонусных игр должно быть больше нуля";
            return false;
        }

        error = string.Empty;
        return true;
    }
}