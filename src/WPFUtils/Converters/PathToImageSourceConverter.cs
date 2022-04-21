using System;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace WPFUtils.Converters
{
    public class PathToImageSourceConverter : BaseConverter<PathToImageSourceConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path)
            {
                return new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            }

            return base.Convert(value, targetType, parameter, culture);
        }
    }
}
