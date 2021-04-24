using System;
using Windows.UI.Xaml.Data;

namespace Peppa.Converters
{
    public class ConvertDateTimeOffsetToDateTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset date = (DateTimeOffset)value;

            return date.Date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
