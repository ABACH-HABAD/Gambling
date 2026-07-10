using System.Windows.Media.Imaging;

namespace Gambling.Wpf.User.Classes;

internal class BitmapCreator
{
    public static BitmapImage CreateBitmap(Uri uri)
    {
        BitmapImage bitmap = new();
        bitmap.BeginInit();
        bitmap.UriSource = uri;
        bitmap.EndInit();
        return bitmap;
    }
}