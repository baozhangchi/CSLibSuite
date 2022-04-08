namespace SVNUtils.CryptSharp.Utility
{
    /// <summary>
    /// Base-64 binary-to-text encodings.
    /// </summary>
    // Token: 0x02000007 RID: 7
    public static class Base64Encoding
    {
        /// <summary>
        /// Blowfish crypt orders characters differently from standard crypt, and begins encoding from
        /// the most-significant bit instead of the least-significant bit.
        /// </summary>
        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000011 RID: 17 RVA: 0x00002710 File Offset: 0x00000910
        // (set) Token: 0x06000012 RID: 18 RVA: 0x00002726 File Offset: 0x00000926
        public static BaseEncoding Blowfish { get; private set; } = new BaseEncoding("./ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", true);

        /// <summary>
        /// Traditional DES crypt base-64, as seen on Unix /etc/passwd, many websites, database servers, etc.
        /// </summary>
        // Token: 0x17000003 RID: 3
        // (get) Token: 0x06000013 RID: 19 RVA: 0x00002730 File Offset: 0x00000930
        // (set) Token: 0x06000014 RID: 20 RVA: 0x00002746 File Offset: 0x00000946
        public static BaseEncoding UnixCrypt { get; private set; } = new BaseEncoding("./0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", true);

        /// <summary>
        /// MD5, SHA256, and SHA512 crypt base-64, as seen on Unix /etc/passwd, many websites, database servers, etc.
        /// </summary>
        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000015 RID: 21 RVA: 0x00002750 File Offset: 0x00000950
        // (set) Token: 0x06000016 RID: 22 RVA: 0x00002766 File Offset: 0x00000966
        public static BaseEncoding UnixMD5 { get; private set; } = new BaseEncoding("./0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", false);
    }
}
