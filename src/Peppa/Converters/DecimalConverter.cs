using System;
using Windows.UI.Xaml.Data;

namespace Peppa.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
            => value.ToString();

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            decimal.TryParse((string)value, out decimal result);
            return result;
        }
    }
}
