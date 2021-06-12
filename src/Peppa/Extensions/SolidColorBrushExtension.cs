using System;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Peppa.Extensions
{
    public static class SolidColorBrushExtension
    {
        public static string ToHexColor(this Color color)
            //TODO Remove alpha from color
            => color.ToString().Replace("#FF", "#");

        public static string ToHexColor(this SolidColorBrush color)
            //TODO Remove alpha from color
            => color.ToString().Replace("#FF", "#");

        public static Color ToColor(this string hexString)
        {
            hexString = hexString.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hexString.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hexString.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hexString.Substring(4, 2), 16));
            return Color.FromArgb(byte.Parse("255"), r, g, b);
        }

        public static SolidColorBrush ToSolidColorBrush(this string hexString)
        {
            hexString = hexString.Replace("#", string.Empty);
            byte r = (byte)Convert.ToUInt32(hexString.Substring(0, 2), 16);
            byte g = (byte)Convert.ToUInt32(hexString.Substring(2, 2), 16);
            byte b = (byte)Convert.ToUInt32(hexString.Substring(4, 2), 16);
            return new SolidColorBrush(Color.FromArgb(byte.Parse("255"), r, g, b));
        }
    }
}