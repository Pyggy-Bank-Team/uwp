using Windows.UI.Xaml.Media;

namespace piggy_bank_uwp.Extensions
{
    public static class SolidColorBrushExtension
    {
        public static string ToColor(this SolidColorBrush brush)
            => brush.Color.ToString();
    }
}