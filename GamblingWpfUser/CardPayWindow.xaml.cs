using System.Windows;
using System.Windows.Controls;
using BusinessLogic.Account.Balance;
using BusinessLogic.Validation;

namespace GamblingWpfUser
{
    /// <summary>
    /// Логика взаимодействия для CardPayWindow.xaml
    /// </summary>
    public partial class PayWindow : Window
    {
        private readonly ICardValidation _cardValidation;
        private readonly ICardPayService _cardPayService;
        private string? CvvCvc;
        private string? Year;
        private string? Month;
        private string? CardNumber;

        public PayWindow(ICardValidation cardValidation, ICardPayService cardPayService)
        {
            _cardValidation = cardValidation;
            _cardPayService = cardPayService;
            InitializeComponent();
        }

        private void CvvCvC_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CvvCvcTextBox.Text.Length > 3)
            {
                CvvCvcTextBox.Text = CvvCvc;
                CvvCvcTextBox.CaretIndex = 3;
            }
            else CvvCvc = CvvCvcTextBox.Text;
        }

        private void Month_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MonthText.Text.Length > 2)
            {
                MonthText.Text = Month;
                MonthText.CaretIndex = 2;
            }
            else Month = MonthText.Text;
        }

        private void Year_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (YearText.Text.Length > 2)
            {
                YearText.Text = Year;
                YearText.CaretIndex = 2;
            }
            else Year = YearText.Text;
        }

        private void CardNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CardNumberText.Text.Length > 16)
            {
                CardNumberText.Text = CardNumber;
                CardNumberText.CaretIndex = 16;
            }
            else CardNumber = CardNumberText.Text;
        }

        private async void PayButton_Click(object sender, RoutedEventArgs e)
        {
            PayCard card = new(CardNumber, Year, Month, CardUserText.Text, CvvCvc);
            if (_cardValidation.CardValidation(card, out string error))
            {
                if (double.TryParse(PayCount.Text, out double payCount) || payCount > 0) {
                    if (await _cardPayService.AddToBalanceAsync(0, payCount, card, Promocode.Text != string.Empty || Promocode.Text != null ? Promocode.Text : null))
                    {
                        PayButton.IsEnabled = false;
                        MessageBox.Show(error);
                        await Task.Delay(5000);
                        await MainWindow.Instance.UpdateProfileInfo();
                        MessageBox.Show("Оплата прошла успешно");
                        PayButton.IsEnabled = true;
                        Close();
                    }
                    else MessageBox.Show("Ошибка пополнения");
                }
                else MessageBox.Show("Введите сумму пополнения");
            }
            else
            {
                MessageBox.Show(error);
            }
        }
    }
}
