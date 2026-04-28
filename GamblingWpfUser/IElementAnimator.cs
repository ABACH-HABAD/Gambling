using System.Windows;

namespace GamblingWpfUser;

internal interface IElementAnimator
{
    internal Task<bool> AnimateElement(UIElement element, double from, double to, double duration);
}
