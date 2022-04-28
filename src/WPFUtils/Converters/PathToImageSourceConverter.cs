using System;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace WPFUtils.Converters
{
    public class PathToImageSourceConverter : BaseConverter<PathToImageSourceConverter>
    {
        protected override object NormalConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path)
            {
                return new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            }

            return default;
        }
    }
}
