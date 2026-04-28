using System.Windows.Controls;

namespace GamblingWpfAdmin.Navigation;

public interface IPageGetter
{
    public Page GetPage<T>() where T : Page;
}
