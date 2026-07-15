using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Gambling.Core.Models;

namespace Gambling.Wpf.Admin.Windows.UserDataTableWindows;

/// <summary>
/// Логика взаимодействия для PromocodeActivatesViewerWindow.xaml
/// </summary>
public partial class PromocodeActivatesViewerWindow : Window
{
    public PromocodeActivatesViewerWindow()
    {
        InitializeComponent();
    }

    public void ViewPromocodeActivation(PromotionalCodeModel promotionalCodeEntity)
    {
        Title = "Пользователи, активировавшие промокод " + promotionalCodeEntity.Code;

        ObservableCollection<UserModel> users = [];
        foreach (PromotionalCodesActivateModel activate in promotionalCodeEntity.Activates)
        {
            if (activate.User != null) users.Add(activate.User);
        }
        UsersDataGrid.ItemsSource = users;
    }

    private void CheckStatistics_Click(object sender, RoutedEventArgs e)
    {
        UserStatisticWindow statisticsWindow = App.Services.GetRequiredService<UserStatisticWindow>();
        statisticsWindow.ShowDialog();
    }
}