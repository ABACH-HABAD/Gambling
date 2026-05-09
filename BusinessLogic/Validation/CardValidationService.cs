using BusinessLogic.Auth;
using BusinessLogic.Balance;
using Org.BouncyCastle.Asn1.X509;
using System.Text.RegularExpressions;

namespace BusinessLogic.Validation;

public partial class CardValidationService : BaseValidation, ICardValidation
{
    [GeneratedRegex(@"^[A-Z]+\s[A-Z]+$")]
    private static partial Regex CardOwnerRegex();

    [GeneratedRegex(@"^\d{16}$")]
    private static partial Regex CardNumberRegex();

    public bool CardValidation(PayCard card, out string error)
    {
        string? CardNumber = card.CardNumber;
        string? Year = card.Year;
        string? Month = card.Month;
        string? Owner = card.Owner;
        string? CvvCvc = card.CvvCvc;

        if (CardNumber == null || CardNumber == string.Empty || CardOwnerRegex().IsMatch(CardNumber))
        {
            error = "Введите номер банковской карты";
            return false;
        }
        else
        {
            if (!Validate(CardNumber, out string cardNumberError))
            {
                error = cardNumberError;
                return false;
            }
        }

        if (Year == null || Year == string.Empty || !int.TryParse(Year, out _))
        {
            error = "Введите год окончания срока обслуживания карты";
            return false;
        }

        if (Month == null || Month == string.Empty || !int.TryParse(Month, out int mnt) || mnt > 12)
        {
            
            error = "Введите месяц окончания срока обслуживания карты";
            return false;
        }

        if (Owner == null || Owner == string.Empty)
        {
            error = "Имя и фамилию пользователя карты латинскими буквами";
            return false;
        }
        else
        {
            if (!CardOwnerRegex().IsMatch(Owner))
            {
                error = "Имя и фамилию пользователя карты латинскими буквами";
                return false;
            }
        }

        if (CvvCvc == null || CvvCvc == string.Empty || !int.TryParse(CvvCvc, out _))
        {
            error = "Введите 3-х значный Cvv/Cvc код с обратной стороны карты";
            return false;
        }

        error = "Оплата производится, пожалуйста подождите!";
        return true;
    }

    public override bool Validate(string input, out string error)
    {
        error = string.Empty;

        int sum = 0;
        foreach (char c in input)
        {
            int number = (int)char.GetNumericValue(c);

            if (number % 2 == 0)
            {
                number *= 2;
                if (number > 9)
                {
                    string bigNumber = number.ToString();
                    foreach (char oneNumber in bigNumber)
                    {
                        sum += (int)char.GetNumericValue(oneNumber);
                    }

                }
                sum += number;
            }
            else continue;
        }

        if (sum % 10 == 0) return true;
        else
        {
            error = "Карта с таким номером не существует";
            return false;
        }
    }
}
