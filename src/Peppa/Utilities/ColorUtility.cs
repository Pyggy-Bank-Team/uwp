﻿using System.Text;
using Windows.UI;

namespace Peppa.Utilities
{
    public static class ColorUtility
    {
        public static Color GetColorFromHexString(string hexString)
        {
            hexString = hexString.Replace("#", "");
            byte a = byte.Parse(hexString.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte r = byte.Parse(hexString.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hexString.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(255, a, r, g);
        }

        public static string GetHexStringFromColor(Color color)
        {
            StringBuilder hexColor = new StringBuilder("#");

            hexColor.Append(color.R.ToString());
            hexColor.Append(color.G.ToString());
            hexColor.Append(color.B.ToString());

            return hexColor.ToString();
        }
    }
}
