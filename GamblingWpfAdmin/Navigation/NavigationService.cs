using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace GamblingWpfAdmin.Navigation;

public class NavigationService : INavigationService, IPageGetter
{
    private Frame? _navigationFrame;

    public void SetFrame(Frame frame)
    {
        _navigationFrame = frame;
    }

    public void NavigateTo<T>() where T : Page
    {
        if (_navigationFrame == null) throw new InvalidOperationException("Frame не установлен, для установки вызовете SetFrame(Frame)");
        Page p = GetPage<T>();
        _navigationFrame.Navigate(p);
    }

    public Page GetPage<T>() where T : Page
    {
        Page? p;
        try
        {
            p = App.Services.GetService<T>();
        }
        catch
        {
            throw new InvalidOperationException();
        }
            
        if (p == null) 
            throw new InvalidOperationException();
        return p;

    }
}
