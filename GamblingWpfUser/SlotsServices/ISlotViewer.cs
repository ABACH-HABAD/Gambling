using BusinessLogic.Game.Slots;
using System.Windows;
using System.Windows.Controls;

namespace GamblingWpfUser.SlotsServices;

internal interface ISlotViewer
{
    internal List<List<SlotElement>> StartSlots { get; }

    internal List<List<SlotElement>> GenerateRandomSlots(int lines = 3, int columns = 5);
    internal Task<List<UIElement>> GetCurrentElements(Grid grid);
    internal Task<List<UIElement>> ViewSlots(List<List<SlotElement>> elements, bool drawCombos = false);
    internal Task ViewSlotsWithoutAnimation(Grid grid, List<UIElement> newElements);
    internal Task ViewSpinSlots(Grid grid, List<UIElement> currentElements, List<UIElement> newElements, double duration = 0.5, int delay = 0);
}
