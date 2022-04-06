using System.Linq;

namespace System
{
    public static class ObjectExtensions
    {
        public static long ToLong(this Guid guid) => BitConverter.ToInt64(guid.ToByteArray(), 0);

        public static string ToHex(this int value, bool toUpper = false) => toUpper ? Convert.ToString(value, 16).ToUpper() : Convert.ToString(value, 16);

        public static string ToBinary(this int value, int? length = null) => length.HasValue ? Convert.ToString(value, 2).PadLeft(length.Value, '0') : Convert.ToString(value, 2);

        public static bool[] ToBinaryArray(this int value, int? length = null) => value.ToBinary(length).Select(x => x == '1').ToArray();
    }
}
