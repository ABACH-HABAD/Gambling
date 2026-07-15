using System.Windows.Controls;

namespace Gambling.Wpf.Admin.Abstractions;

public interface IPageGetter
{
    public Page GetPage<T>() where T : Page;
}