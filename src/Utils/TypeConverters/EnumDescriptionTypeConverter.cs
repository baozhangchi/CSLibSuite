using System.Globalization;

// ReSharper disable once CheckNamespace
namespace System.ComponentModel
{
    public class EnumDescriptionTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    var type = value.GetType();
                    if (type.IsEnum)
                    {
                        return ((Enum)value).GetDescription() ?? value.ToString();
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
