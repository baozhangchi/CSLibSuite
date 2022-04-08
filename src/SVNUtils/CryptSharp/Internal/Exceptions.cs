using System;

namespace SVNUtils.CryptSharp.Internal
{
    // Token: 0x02000021 RID: 33
    internal static class Exceptions
    {
        // Token: 0x06000100 RID: 256 RVA: 0x00008C88 File Offset: 0x00006E88
        public static ArgumentException Argument(string valueName, string message, params object[] args)
        {
            message = string.Format(message, args);
            return valueName == null ? new ArgumentException(message) : new ArgumentException(message, valueName);
        }

        // Token: 0x06000101 RID: 257 RVA: 0x00008CB8 File Offset: 0x00006EB8
        public static ArgumentNullException ArgumentNull(string valueName)
        {
            return valueName == null ? new ArgumentNullException() : new ArgumentNullException(valueName);
        }

        // Token: 0x06000102 RID: 258 RVA: 0x00008CDC File Offset: 0x00006EDC
        public static ArgumentOutOfRangeException ArgumentOutOfRange(string valueName, string message, params object[] args)
        {
            message = string.Format(message, args);
            return valueName == null ? new ArgumentOutOfRangeException(message) : new ArgumentOutOfRangeException(valueName, message);
        }

        // Token: 0x06000103 RID: 259 RVA: 0x00008D0C File Offset: 0x00006F0C
        public static InvalidOperationException InvalidOperation()
        {
            return new InvalidOperationException();
        }

        // Token: 0x06000104 RID: 260 RVA: 0x00008D28 File Offset: 0x00006F28
        public static NotSupportedException NotSupported()
        {
            return new NotSupportedException();
        }
    }
}
