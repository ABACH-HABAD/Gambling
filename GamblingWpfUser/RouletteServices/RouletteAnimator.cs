using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GamblingWpfUser.RouletteServices;

internal class RouletteAnimator : IElementAnimator
{
    Task<bool> IElementAnimator.AnimateElement(UIElement element, double from, double to, double duration)
    {
        TaskCompletionSource<bool> tcs = new();

        RotateTransform rotate = new();
        element.RenderTransform = rotate;
        element.RenderTransformOrigin = new Point(0.5, 0.5);

        DoubleAnimation animation = new()
        {
            From = from,
            To = to,
            Duration = TimeSpan.FromSeconds(duration),
        };

        EventHandler handler = null!;
        handler = (s, e) =>
        {
            animation.Completed -= handler;
            tcs.SetResult(true);
        };

        animation.Completed += handler;

        rotate.BeginAnimation(RotateTransform.AngleProperty, animation);

        return tcs.Task;
    }
}
