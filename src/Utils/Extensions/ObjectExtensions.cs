using System.Linq;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// GUID转Int64
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static long ToLong(this Guid guid) => BitConverter.ToInt64(guid.ToByteArray(), 0);

        /// <summary>
        /// Int32转十六进制字符串
        /// </summary>
        /// <param name="value">输入数字</param>
        /// <param name="toUpper">是否转大写</param>
        /// <returns></returns>
        public static string ToHex(this int value, bool toUpper = false) => toUpper ? Convert.ToString(value, 16).ToUpper() : Convert.ToString(value, 16);

        /// <summary>
        /// Int32转二进制字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToBinary(this int value, int? length = null) => length.HasValue ? Convert.ToString(value, 2).PadLeft(length.Value, '0') : Convert.ToString(value, 2);

        /// <summary>
        /// Int32转二进制数组
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool[] ToBinaryArray(this int value, int? length = null) => value.ToBinary(length).Select(x => x == '1').ToArray();

        /// <summary>
        /// 保留n位小数，不进行四舍五入
        /// </summary>
        /// <param name="value">原数字</param>
        /// <param name="digit">小数位数</param>
        /// <returns>结果</returns>
        public static double KeepDecimal(this double value, uint digit = 0)
        {
            var n = Math.Pow(10, digit);
            return (int)(value * n) / n;
        }
    }
}
