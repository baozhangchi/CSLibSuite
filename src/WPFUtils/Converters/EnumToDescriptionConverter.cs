using System;
using System.Globalization;

namespace WPFUtils.Converters
{
    public class EnumToDescriptionConverter : BaseConverter<EnumToDescriptionConverter>
    {
        protected override object NormalConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (value is Enum @enum)
            {
                return @enum.GetDescription();
            }

            return base.NormalConvert(value, targetType, parameter, culture);
        }
    }
}
