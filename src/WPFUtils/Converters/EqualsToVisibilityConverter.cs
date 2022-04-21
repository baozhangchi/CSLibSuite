using System;
using System.Globalization;
using System.Windows;

namespace WPFUtils.Converters
{
    public class EqualsToVisibilityConverter : BaseConverter<EqualsToVisibilityConverter>
    {
        private bool _invert;
        private static EqualsToVisibilityConverter _invertInstance;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_invert)
            {
                return (value == null && parameter == null) ||
                       (value != null && parameter != null && value.Equals(parameter))
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
            else
            {
                return (value == null && parameter == null) ||
                       (value != null && parameter != null && value.Equals(parameter))
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public static EqualsToVisibilityConverter InvertInstance
        {
            get
            {
                if (_invertInstance == null)
                {
                    lock (_lock)
                    {
                        if (_invertInstance == null)
                        {
                            _invertInstance = new EqualsToVisibilityConverter();
                            _invertInstance._invert = true;
                        }
                    }
                }

                return _invertInstance;
            }
        }
    }
}
