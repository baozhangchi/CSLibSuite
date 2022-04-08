using System;

namespace SVNUtils.CryptSharp
{
    /// <summary>
    /// Options that modify the crypt operation.
    /// </summary>
    public class CrypterOption
    {
        /// <summary>
        /// Create instance
        /// </summary>
        protected CrypterOption()
        {
        }

        /// <summary>
        /// The number of rounds to iterate.
        /// </summary>
        public static readonly CrypterOptionKey Rounds = new CrypterOptionKey("Rounds", typeof(int));

        /// <summary>
        /// The variant of the crypt algorithm to use.
        /// </summary>
        public static readonly CrypterOptionKey Variant = new CrypterOptionKey("Variant", typeof(Enum));
    }
}
