using System;
using Windows.UI;

namespace Peppa.Extensions
{
    public static class SolidColorBrushExtension
    {
        public static string ToColor(this Color color)
            //TODO Remove alpha from color
            => color.ToString().Replace("#FF", "#");

        public static Color GetColor(this string hexString)
        {
            hexString = hexString.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hexString.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hexString.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hexString.Substring(4, 2), 16));
            return Color.FromArgb(byte.Parse("100"), r, g, b);
        }
    }
}