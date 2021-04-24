using System;
using Windows.UI.Xaml.Data;
using Peppa.Enums;

namespace Peppa.Converters
{
    public class AccountTypeToFronIconValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var type = Enum.Parse<AccountType>(value.ToString());
            switch (type)
            {
                case AccountType.Card:
                    return (char)System.Convert.ToInt32("E8C7", 16);
                case AccountType.Cash:
                    return (char)System.Convert.ToInt32("0024", 16);
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => value;
    }
}
