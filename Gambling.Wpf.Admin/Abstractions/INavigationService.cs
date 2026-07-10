using System.Windows.Controls;

namespace Gambling.Wpf.Admin.Abstractions;

public interface INavigationService
{
    public void SetFrame(Frame frame);
    public void NavigateTo<T>() where T : Page;
}