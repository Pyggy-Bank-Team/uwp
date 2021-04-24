using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Peppa.Enums;

namespace Peppa.Converters
{
    public class AccountTypeToFontValueConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var type = Enum.Parse<AccountType>(value.ToString());
            switch (type)
            {
                case AccountType.Cash:
                    return new FontFamily("Segoe UI");
                default:
                    return new FontFamily("Segoe MDL2 Assets");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => value;
    }
}
