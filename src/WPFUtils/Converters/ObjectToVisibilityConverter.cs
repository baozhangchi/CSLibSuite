using System;
using System.Globalization;
using System.Windows;

namespace WPFUtils.Converters
{
    public class ObjectToVisibilityConverter : BaseConverter<ObjectToVisibilityConverter>
    {
        protected override object InvertConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return string.IsNullOrWhiteSpace(str) ? Visibility.Visible : Visibility.Collapsed;
            }

            return value == null ? Visibility.Visible : Visibility.Collapsed;
        }

        protected override object NormalConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return string.IsNullOrWhiteSpace(str) ? Visibility.Collapsed : Visibility.Visible;
            }

            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
