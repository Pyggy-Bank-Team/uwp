using Windows.UI;

namespace Peppa.Extensions
{
    public static class SolidColorBrushExtension
    {
        public static string ToColor(this Color color)
            //TODO Remove alpha from color
            => color.ToString().Replace("#FF", "#");
    }
}