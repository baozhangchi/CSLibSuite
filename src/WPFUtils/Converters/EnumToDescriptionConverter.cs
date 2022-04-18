using System;
using System.Globalization;

namespace WPFUtils.Converters
{
    public class EnumToDescriptionConverter : BaseConverter<EnumToDescriptionConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if(value is Enum @enum)
            {
                return @enum.GetDescription();
            }

            return base.Convert(value, targetType, parameter, culture);
        }
    }
}
