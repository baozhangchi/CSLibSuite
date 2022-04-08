using System;

namespace SVNUtils.CryptSharp.Internal
{
    // Token: 0x02000015 RID: 21
    internal static class ByteArray
    {
        // Token: 0x0600009D RID: 157 RVA: 0x00004D44 File Offset: 0x00002F44
        public static int NullTerminatedLength(byte[] buffer, int maxLength)
        {
            if (maxLength > buffer.Length)
            {
                maxLength = buffer.Length;
            }
            int length = 0;
            while (length < maxLength && buffer[length] != 0)
            {
                length++;
            }
            return length;
        }

        // Token: 0x0600009E RID: 158 RVA: 0x00004D88 File Offset: 0x00002F88
        public static byte[] TruncateAndCopy(byte[] buffer, int maxLength)
        {
            byte[] truncatedBuffer = new byte[Math.Min(buffer.Length, maxLength)];
            Array.Copy(buffer, truncatedBuffer, truncatedBuffer.Length);
            return truncatedBuffer;
        }
    }
}
