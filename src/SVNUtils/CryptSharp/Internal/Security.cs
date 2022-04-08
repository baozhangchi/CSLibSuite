using System;
using System.Security.Cryptography;

namespace SVNUtils.CryptSharp.Internal
{
    // Token: 0x02000016 RID: 22
    internal static class Security
    {
        // Token: 0x0600009F RID: 159 RVA: 0x00004DB8 File Offset: 0x00002FB8
        public static void Clear(Array array)
        {
            if (array != null)
            {
                Array.Clear(array, 0, array.Length);
            }
        }

        // Token: 0x060000A0 RID: 160 RVA: 0x00004DE0 File Offset: 0x00002FE0
        public static byte[] GenerateRandomBytes(int count)
        {
            Check.Range("count", count, 0, int.MaxValue);
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[count];
            rng.GetBytes(bytes);
            return bytes;
        }
    }
}
