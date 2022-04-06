using System;
using System.Globalization;
using System.Windows;

namespace WPFUtils.Converters
{
    public class ObjectToVisibilityConverter : BaseConverter<ObjectToVisibilityConverter>
    {
        private bool invert;
        private static ObjectToVisibilityConverter _invertInstance;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (invert)
            {
                if (value is string str)
                {
                    return string.IsNullOrWhiteSpace(str) ? Visibility.Visible : Visibility.Collapsed;
                }

                return value == null ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                if (value is string str)
                {
                    return string.IsNullOrWhiteSpace(str) ? Visibility.Collapsed : Visibility.Visible;
                }

                return value == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public static ObjectToVisibilityConverter InvertInstance
        {
            get
            {
                if (_invertInstance == null)
                {
                    lock (_lock)
                    {
                        if (_invertInstance == null)
                        {
                            _invertInstance = new ObjectToVisibilityConverter();
                            _invertInstance.invert = true;
                        }
                    }
                }

                return _invertInstance;
            }
        }
    }
}
