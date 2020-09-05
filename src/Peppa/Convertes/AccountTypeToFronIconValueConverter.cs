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
                    return "/Assets/Icons/payment.svg";
                case AccountType.Cash:
                    return "/Assets/Icons/money.svg";
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => value;
    }
}
