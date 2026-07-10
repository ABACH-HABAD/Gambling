using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Core.Models;

namespace Gambling.Wpf.Admin.Windows.UserDataTableWindows;

/// <summary>
/// Логика взаимодействия для UserDataWindow.xaml
/// </summary>
public partial class UserDataWindow : Window
{
    private UserModel? _user;

    public UserDataWindow()
    {
        InitializeComponent();
    }

    public void DisplayUserData(UserModel user)
    {
        _user = user;

        Title = "Таблица данных пользователя " + _user.Name;

        Id.Text = user.Id.ToString();
        Email.Text = user.Email.ToString();
        DisplayName.Text = user.Name.ToString();
        Role.Text = user.DisplayRole;
        IsBlocked.Text = user.DisplayIsBlocked;
        Balance.Text = user.Balance.ToString();
        Coefficient.Text = user.Coefficient.ToString();
    }

    private void StatisticButton_Click(object sender, RoutedEventArgs e)
    {
        UserStatisticWindow userStatisticWindow = App.Services.GetRequiredService<UserStatisticWindow>();
        if (_user != null)
        {
            userStatisticWindow.SetUser(_user);
            userStatisticWindow.ShowDialog();
        }
        else MessageBox.Show("Ошибка при отображении статистики пользователя");
    }
}