using System.Collections.Generic;
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

        public static T GetAttribute<T>(this Enum @enum)
        where T : Attribute
        {
            var type = @enum.GetType();
            var fieldInfo = type.GetField(@enum.ToString());
            var attribute = fieldInfo?.GetCustomAttribute<T>();
            return attribute;
        }

        public static IEnumerable<T> GetAttributes<T>(this Enum @enum)
            where T : Attribute
        {
            var type = @enum.GetType();
            var fieldInfo = type.GetField(@enum.ToString());
            var attributes = fieldInfo?.GetCustomAttributes<T>();
            return attributes;
        }
    }
}
