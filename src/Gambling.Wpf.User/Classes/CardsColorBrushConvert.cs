using Gambling.Application.Core.BusinessModels.GameModels.Blackjack;
using System.Windows.Media;

namespace Gambling.Wpf.User.Classes;

internal static class CardsColorBrushConvert
{
    private static SolidColorBrush RedBrush {  get; } = new SolidColorBrush(Colors.Red);
    private static SolidColorBrush BlackBrush { get; } = new SolidColorBrush(Colors.Black);

    internal static SolidColorBrush DisplayColorBrush(this Card card) => card.IsRed ? RedBrush : BlackBrush;
}