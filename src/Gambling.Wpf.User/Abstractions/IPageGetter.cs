using System.Windows.Controls;

namespace Gambling.Wpf.User.Abstractions;

internal interface IPageGetter
{
    internal Page GetPage<T>() where T : Page;
}