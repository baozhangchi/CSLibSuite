using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
