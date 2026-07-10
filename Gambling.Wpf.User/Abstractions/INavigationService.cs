using System.Windows.Controls;

namespace Gambling.Wpf.User.Abstractions;

public interface INavigationService
{
    public Type CurrentPageType { get; }
    public void SetFrame(Frame frame);
    public void NavigateTo<T>() where T : Page;
}