using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Peppa.Enums;

namespace Peppa.Converters
{
    public class ViewTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is OperationViewType type))
                return Visibility.Collapsed;
            
            if (Enum.TryParse(typeof(OperationViewType), (string)parameter, out var condition))
                return type == (OperationViewType)condition ? Visibility.Visible : Visibility.Collapsed;

            if ((string)parameter == "Budget")
            {
                switch (type)
                {
                    case OperationViewType.Income:
                    case OperationViewType.Expense:
                        return Visibility.Visible;
                    default:
                        return Visibility.Collapsed;
                }
            }
            
            if ((string)parameter == "Refill")
            {
                switch (type)
                {
                    case OperationViewType.Income:
                    case OperationViewType.Transfer:
                        return Visibility.Visible;
                    default:
                        return Visibility.Collapsed;
                }
            }
            
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}