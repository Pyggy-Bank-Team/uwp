using System;
using Windows.UI.Xaml.Data;
using Peppa.Enums;

namespace Peppa.Convertes
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
                default:
                    return (char)System.Convert.ToInt32("EC59", 16);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => value;
    }
}
