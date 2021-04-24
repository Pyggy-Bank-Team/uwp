using System;
using Windows.UI.Xaml.Data;

namespace Peppa.Converters
{
    public class ConvertDateTimeOffsetToDateTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime date)
                return DateTimeOffset.Parse(date.ToString());

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset dateOffset)
                return dateOffset.Date;

            return value;
        }
    }
}
