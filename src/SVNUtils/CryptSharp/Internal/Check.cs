using System;

namespace SVNUtils.CryptSharp.Internal
{
    // Token: 0x02000026 RID: 38
    internal static class Check
    {
        // Token: 0x06000119 RID: 281 RVA: 0x00009248 File Offset: 0x00007448
        public static void Bounds(string valueName, Array value, int offset, int count)
        {
            Null(valueName, value);
            if (offset < 0 || count < 0 || count > value.Length - offset)
            {
                throw Exceptions.ArgumentOutOfRange(valueName, "Range [{0}, {1}) is outside array bounds [0, {2}).", new object[]
                {
                    offset,
                    offset + count,
                    value.Length
                });
            }
        }

        // Token: 0x0600011A RID: 282 RVA: 0x000092B4 File Offset: 0x000074B4
        public static void Length(string valueName, Array value, int minimum, int maximum)
        {
            Null(valueName, value);
            if (value.Length < minimum || value.Length > maximum)
            {
                throw Exceptions.ArgumentOutOfRange(valueName, "Length must be in the range [{0}, {1}].", new object[]
                {
                    minimum,
                    maximum
                });
            }
        }

        // Token: 0x0600011B RID: 283 RVA: 0x00009310 File Offset: 0x00007510
        public static void Null<T>(string valueName, T value)
        {
            if (value == null)
            {
                throw Exceptions.ArgumentNull(valueName);
            }
        }

        // Token: 0x0600011C RID: 284 RVA: 0x00009338 File Offset: 0x00007538
        public static void Range(string valueName, int value, int minimum, int maximum)
        {
            if (value < minimum || value > maximum)
            {
                throw Exceptions.ArgumentOutOfRange(valueName, "Value must be in the range [{0}, {1}].", new object[]
                {
                    minimum,
                    maximum
                });
            }
        }
    }
}
