using System;

namespace SVNUtils.CryptSharp.Internal
{
    // Token: 0x02000022 RID: 34
    internal static class BitMath
    {
        // Token: 0x06000105 RID: 261 RVA: 0x00008D40 File Offset: 0x00006F40
        public static int CountLeadingZeros(int value)
        {
            int count = 0;
            while (count < 32 && 0L == (value & (long)(ulong)(2147483648U >> count)))
            {
                count++;
            }
            return count;
        }

        // Token: 0x06000106 RID: 262 RVA: 0x00008D7C File Offset: 0x00006F7C
        public static bool IsPositivePowerOf2(int value)
        {
            return 0 < value && 0 == (value & value - 1);
        }

        // Token: 0x06000107 RID: 263 RVA: 0x00008DA0 File Offset: 0x00006FA0
        public static byte ReverseBits(byte value)
        {
            return (byte)((value * Convert.ToUInt64(-2145384446) & 36578664720UL) * 4311810305UL >> 32);
        }

        // Token: 0x06000108 RID: 264 RVA: 0x00008DD8 File Offset: 0x00006FD8
        public static byte ShiftLeft(byte value, int bits)
        {
            return (byte)(bits > 0 ? value << bits : value >> -bits);
        }

        // Token: 0x06000109 RID: 265 RVA: 0x00008E00 File Offset: 0x00007000
        public static byte ShiftRight(byte value, int bits)
        {
            return ShiftLeft(value, -bits);
        }

        // Token: 0x0600010A RID: 266 RVA: 0x00008E1C File Offset: 0x0000701C
        public static void Swap<T>(ref T left, ref T right)
        {
            T temp = right;
            right = left;
            left = temp;
        }
    }
}
