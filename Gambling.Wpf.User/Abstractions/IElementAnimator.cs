using System.Windows;

namespace Gambling.Wpf.User.Abstractions;

internal interface IElementAnimator
{
    internal Task<bool> AnimateElement(UIElement element, double from, double to, double duration);
}