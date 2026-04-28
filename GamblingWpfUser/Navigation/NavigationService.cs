using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace GamblingWpfUser.Navigation;

internal class NavigationService : INavigationService, IPageGetter
{
    private Type currentPageType;
    private Frame? _navigationFrame;

    public Type CurrentPageType => currentPageType;

    public NavigationService()
    {
        currentPageType = typeof(MainWindow);
    }

    public void SetFrame(Frame frame)
    {
        _navigationFrame = frame;
    }

    public void NavigateTo<T>() where T : Page
    {
        currentPageType = typeof(T);
        if (_navigationFrame == null) throw new InvalidOperationException("Frame не установлен, для установки вызовете SetFrame(Frame)");
        Page page = GetPage<T>();
        _navigationFrame.Navigate(page);
        MainWindow.Instance.ChangeTitle(page.Title);
    }

    public Page GetPage<T>() where T : Page
    {
        return App.Services.GetRequiredService<T>();
    }
}
