using BusinessLogic.Game.Slots;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GamblingWpfUser.SlotsServices;

internal class SlotViewer(IElementAnimator slotAnimator, ISlotsCombinationCounterService slotsCombinationCounterService) : ISlotViewer
{
    internal static readonly Dictionary<string, string> SlotsUriImages = new()
    {
        {"Ананас", "/Resources/Images/Slots/ананас.jpg" },
        {"Апельсин", "/Resources/Images/Slots/апельсин.jpg" },
        {"Арбуз", "/Resources/Images/Slots/арбуз.jpg" },
        {"Виноград", "/Resources/Images/Slots/виноград.jpg" },
        {"Лемон", "/Resources/Images/Slots/лимон.jpg" },
        {"Яблоко", "/Resources/Images/Slots/яблоко.jpg" },
        {"Вишня", "/Resources/Images/Slots/вишня.jpg" },
        {"Звезда", "/Resources/Images/Slots/звезда.jpg" },
        {"BAR", "/Resources/Images/Slots/bar.jpg" },
        {"seven", "/Resources/Images/Slots/7.jpg" },
        {SlotElement.WILD, "/Resources/Images/Slots/wild.jpg" },
        {SlotElement.BONUS, "/Resources/Images/Slots/bonus.jpg" }
    };

    internal static List<List<SlotElement>> StartSlots =
    [
        [new(SlotElement.BONUS),new("Вишня"),new("Вишня"),new("Вишня"),new(SlotElement.BONUS)],
        [new(SlotElement.WILD),new("seven"),new("seven"),new("seven"),new(SlotElement.WILD)],
        [new(SlotElement.BONUS),new("Вишня"),new("Вишня"),new("Вишня"),new(SlotElement.BONUS)],
    ];

    internal static readonly Dictionary<string, BitmapImage> ImagesChach = [];

    List<List<SlotElement>> ISlotViewer.StartSlots => StartSlots;

    List<List<SlotElement>> ISlotViewer.GenerateRandomSlots(int lines, int columns) => GenerateRandomSlots(lines, columns);
    Task<List<UIElement>> ISlotViewer.GetCurrentElements(Grid grid) => GetCurrentElements(grid);
    Task<List<UIElement>> ISlotViewer.ViewSlots(List<List<SlotElement>> elements, bool drawCombos) => ViewSlots(elements, drawCombos);
    Task ISlotViewer.ViewSlotsWithoutAnimation(Grid grid, List<UIElement> newElements) => ViewSlotsWithoutAnimation(grid, newElements);
    Task ISlotViewer.ViewSpinSlots(Grid grid, List<UIElement> currentElements, List<UIElement> newElements, double duration, int delay) => ViewSpinSlots(grid, currentElements, newElements, duration, delay);

    private readonly IElementAnimator _slotAnimator = slotAnimator;
    private readonly ISlotsCombinationCounterService _slotsCombinationCounterService = slotsCombinationCounterService;

    private static List<List<SlotElement>> GenerateRandomSlots(int lines = 3, int columns = 5)
    {
        List<List<SlotElement>> slots = [];
        for (int i = 0; i < lines; i++)
        {
            slots.Add([]);
            for (int j = 0; j < columns; j++)
            {
                slots[i].Add(new SlotElement());
            }
        }
        return slots;
    }

    private async Task<List<UIElement>> GetCurrentElements(Grid grid)
    {
        if (grid.Children.Count == 0) return await ViewSlots(StartSlots);
        else
        {
            List<UIElement> elements = [];
            for (int i = 0; i < grid.Children.Count; i++) elements.Add(grid.Children[i]);
            return elements;
        }
    }

    private async Task<List<UIElement>> ViewSlots(List<List<SlotElement>> elements, bool drawCombos = false)
    {
        List<UIElement> slots = [];

        for (int column = 0; column < elements[0].Count; column++)
        {
            List<SlotElement> columnElements = [];
            for (int line = 0; line < elements.Count; line++)
            {
                columnElements.Add(elements[line][column]);
            }
            SlotsCombo columnCombo = _slotsCombinationCounterService.ComboCount(columnElements);

            Border baraban = new()
            {
                BorderThickness = new Thickness(1),
                Background = new SolidColorBrush(Colors.DarkSlateGray),
            };
            Grid barabanGrid = new();

            for (int line = 0; line < elements.Count; line++)
            {
                barabanGrid.RowDefinitions.Add(new RowDefinition());

                Border cell = new();
                Grid cellGrid = new();

                SlotsCombo lineCombo = _slotsCombinationCounterService.ComboCount(elements[line]);

                //Создаём изображение
                Image image = new()
                {
                    Name = elements[line][column].Text,
                    Stretch = Stretch.Fill
                };

                //Проверяем наличие изображения в кэше
                if (ImagesChach.TryGetValue(elements[line][column].Text, out BitmapImage? value))
                {
                    image.Source = value;
                }

                //Если нет, создаём
                else
                {
                    BitmapImage bitmap = new();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(SlotsUriImages[elements[line][column].Text], UriKind.Relative);
                    bitmap.EndInit();

                    image.Source = bitmap;

                    //Обязательно заносим изображение в кэш
                    ImagesChach.Add(elements[line][column].Text, bitmap);
                }

                cellGrid.Children.Add(image);

                //Добавляем полоски комбинаций
                if (drawCombos)
                {
                    List<int> nums = _slotsCombinationCounterService.ComboNumbersBySymbol(elements[line], lineCombo.Symbol ?? string.Empty);
                    //По горизонтали
                    if (lineCombo.Combo >= 3)
                    {
                        if (nums.Contains(column))
                        {
                            //Создаём изображение
                            image = new()
                            {
                                Stretch = Stretch.Fill
                            };

                            //Проверяем наличие изображения в кэше
                            if (ImagesChach.TryGetValue("HorizontalLine.png", out value))
                            {
                                image.Source = value;
                            }

                            //Если нет, создаём
                            else
                            {
                                BitmapImage bitmap = new();
                                bitmap.BeginInit();
                                bitmap.UriSource = new Uri("/Resources/Images/Slots/HorizontalLine.png", UriKind.Relative);
                                bitmap.EndInit();

                                image.Source = bitmap;

                                //Обязательно заносим изображение в кэш
                                ImagesChach.Add("HorizontalLine.png", bitmap);
                            }

                            cellGrid.Children.Add(image);
                        }
                    }

                    if (columnCombo.Combo >= 3)
                    {
                        //По вертикали
                        if (columnCombo.ComboNumbers.Contains(line))
                        {
                            //Создаём изображение
                            image = new()
                            {
                                Stretch = Stretch.Fill
                            };

                            //Проверяем наличие изображения в кэше
                            if (ImagesChach.TryGetValue("VerticalLine.png", out value))
                            {
                                image.Source = value;
                            }

                            //Если нет, создаём
                            else
                            {
                                BitmapImage bitmap = new();
                                bitmap.BeginInit();
                                bitmap.UriSource = new Uri("/Resources/Images/Slots/VerticalLine.png", UriKind.Relative);
                                bitmap.EndInit();

                                image.Source = bitmap;

                                //Обязательно заносим изображение в кэш
                                ImagesChach.Add("VerticalLine.png", bitmap);
                            }

                            cellGrid.Children.Add(image);
                        }
                    }
                }

                cell.Child = cellGrid;
                cell.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 55, 200));
                cell.BorderThickness = new(1);

                barabanGrid.Children.Add(cell);
                Grid.SetRow(cell, line);
            }

            baraban.Child = barabanGrid;
            slots.Add(baraban);
        }

        return slots;
    }
    private static async Task ViewSlotsWithoutAnimation(Grid grid, List<UIElement> newElements)
    {
        grid.Children.Clear();
        for (int i = 0; i < newElements.Count; i++)
        {
            grid.Children.Add(newElements[i]);
            Grid.SetColumn(newElements[i], i);
        }
    }

    private async Task ViewSpinSlots(Grid grid, List<UIElement> currentElements, List<UIElement> newElements, double duration = 0.5, int delay = 0)
    {
        //Создаем новые элементы
        for (int i = 0; i < newElements.Count; i++)
        {
            UIElement element = newElements[i];
            element.RenderTransform = new TranslateTransform(0, -grid.ActualHeight);
            element.RenderTransformOrigin = new Point(0.5, 0.5);

            Grid.SetColumn(element, i);
            Grid.SetRow(element, 0);
            grid.Children.Add(element);
        }

        List<Task> tasks = [];

        //Анимируем старые элементы вниз
        foreach (UIElement element in currentElements)
        {
            Task task = _slotAnimator.AnimateElement(element, 0, grid.ActualHeight, duration: duration);
            tasks.Add(task);
        }

        //Анимируем новые элементы сверху вниз
        foreach (UIElement element in newElements)
        {
            Task task = _slotAnimator.AnimateElement(element, -grid.ActualHeight, 0, duration: duration);
            tasks.Add(task);
            if (delay > 0) await Task.Delay(delay);
        }

        await Task.WhenAll(tasks);

        //Удаляем старые элементы
        foreach (var oldElement in currentElements)
        {
            grid.Children.Remove(oldElement);
        }
    }
}
