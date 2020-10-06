using Windows.UI.Xaml.Media;

namespace Peppa.Extensions
{
    public static class SolidColorBrushExtension
    {
        public static string ToColor(this SolidColorBrush brush)
            => brush.Color.ToString().Replace("#FF", "#");
    }
}