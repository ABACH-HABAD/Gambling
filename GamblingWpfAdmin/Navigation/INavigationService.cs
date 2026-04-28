using System.Windows.Controls;

namespace GamblingWpfAdmin.Navigation;

public interface INavigationService
{
    public void SetFrame(Frame frame);
    public void NavigateTo<T>() where T : Page;
}
