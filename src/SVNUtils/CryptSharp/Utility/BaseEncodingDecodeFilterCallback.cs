namespace SVNUtils.CryptSharp.Utility
{
    /// <summary>
    /// A callback to map arbitrary characters onto the characters that can be decoded.
    /// </summary>
    /// <param name="originalCharacter">The original character.</param>
    /// <returns>the replacement character.</returns>
    // Token: 0x02000023 RID: 35
    // (Invoke) Token: 0x0600010C RID: 268
    public delegate char BaseEncodingDecodeFilterCallback(char originalCharacter);
}
