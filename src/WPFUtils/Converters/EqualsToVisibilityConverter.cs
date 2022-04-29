using System;
using System.Globalization;
using System.Windows;

namespace WPFUtils.Converters
{
    public class EqualsToVisibilityConverter : BaseConverter<EqualsToVisibilityConverter>
    {
        protected override object NormalConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null && parameter == null) ||
                   (value != null && parameter != null && value.Equals(parameter))
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        protected override object InvertConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null && parameter == null) ||
                   (value != null && parameter != null && value.Equals(parameter))
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
    }
}
