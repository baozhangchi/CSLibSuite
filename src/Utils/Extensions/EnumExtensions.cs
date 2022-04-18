using System.ComponentModel;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum @enum)
        {
            var type = @enum.GetType();
            var fieldInfo = type.GetField(@enum.ToString());
            var attribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description;
        }
    }
}
