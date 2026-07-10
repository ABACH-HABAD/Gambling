using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Gambling.Wpf.User.Abstractions;

namespace Gambling.Wpf.User.Services.SlotsServices;

internal class SlotAnimator : IElementAnimator
{
    Task<bool> IElementAnimator.AnimateElement(UIElement element, double from, double to, double duration)
    {
        TaskCompletionSource<bool> tcs = new();

        DoubleAnimation animation = new()
        {
            From = from,
            To = to,
            Duration = TimeSpan.FromSeconds(duration)
        };

        //Получаем Transform
        TranslateTransform transform;
        if (element.RenderTransform is TranslateTransform existingTransform)
        {
            transform = existingTransform;
        }
        else
        {
            transform = new TranslateTransform();
            element.RenderTransform = transform;
        }

        element.RenderTransformOrigin = new Point(0.5, 0.5);

        //Подписываемся на завершение анимации
        EventHandler handler = null!;
        handler = (s, e) =>
        {
            animation.Completed -= handler;
            tcs.SetResult(true);
        };

        animation.Completed += handler;

        //Запускаем анимацию
        transform.BeginAnimation(TranslateTransform.YProperty, animation);

        return tcs.Task;
    }
}
