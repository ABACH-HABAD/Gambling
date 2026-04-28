using System.Windows.Controls;

namespace GamblingWpfUser.Navigation;

internal interface IPageGetter
{
    internal Page GetPage<T>() where T : Page;
}
