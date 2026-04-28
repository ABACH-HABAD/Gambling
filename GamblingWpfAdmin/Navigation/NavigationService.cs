using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace GamblingWpfAdmin.Navigation;

public class NavigationService(IServiceProvider serviceProvider) : INavigationService, IPageGetter
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private Frame? _navigationFrame;

    public void SetFrame(Frame frame)
    {
        _navigationFrame = frame;
    }

    public void NavigateTo<T>() where T : Page
    {
        if (_navigationFrame == null) throw new InvalidOperationException("Frame не установлен, для установки вызовете SetFrame(Frame)");
        _navigationFrame.Navigate(GetPage<T>());
    }

    public Page GetPage<T>() where T : Page
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}
