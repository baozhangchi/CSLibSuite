using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SVNUtils.CryptSharp.Internal;
using SVNUtils.CryptSharp.Utility;

namespace SVNUtils.CryptSharp
{
    /// <summary>
    /// MD5 crypt, supported by nearly all systems. A variant supports Apache htpasswd files.
    /// </summary>
    public class MD5Crypter //: Crypter
    {
        /// <inheritdoc />
        public string GenerateSalt(CrypterOptions options)
        {
            Check.Null("options", options);
            string prefix;
            switch (options.GetValue(CrypterOption.Variant, MD5CrypterVariant.Standard))
            {
                case MD5CrypterVariant.Standard:
                    prefix = "$1$";
                    break;
                case MD5CrypterVariant.Apache:
                    prefix = "$apr1$";
                    break;
                default:
                    throw Exceptions.ArgumentOutOfRange("CrypterOption.Variant", "Unknown variant.", new object[0]);
            }
            return prefix + Base64Encoding.UnixMD5.GetString(Security.GenerateRandomBytes(6));
        }

        /// <summary>
        /// Creates a one-way password hash (crypted password) from a password string.
        /// Options modify the crypt operation.
        /// </summary>
        /// <param name="password">The password string. Characters are UTF-8 encoded.</param>
        /// <param name="options">Options modifying the crypt operation.</param>
        /// <returns>The crypted password.</returns>
        public string Crypt(string password, CrypterOptions options)
        {
            return Crypt(password, GenerateSalt(options));
        }

        /// <inheritdoc />
        // Token: 0x06000133 RID: 307 RVA: 0x000096B4 File Offset: 0x000078B4
        public bool CanCrypt(string salt)
        {
            Check.Null("salt", salt);
            return salt.StartsWith("$1$") || salt.StartsWith("$apr1$");
        }

        /// <inheritdoc />
        public string Crypt(byte[] password, string salt)
        {
            Check.Null("password", password);
            Check.Null("salt", salt);
            Match match = _regex.Match(salt);
            if (!match.Success)
            {
                throw Exceptions.Argument("salt", "Invalid salt.", new object[0]);
            }
            byte[] prefixBytes = null;
            byte[] saltBytes = null;
            byte[] formattedKey = null;
            byte[] truncatedSalt = null;
            byte[] crypt = null;
            string result2;
            try
            {
                string prefixString = match.Groups["prefix"].Value;
                prefixBytes = Encoding.ASCII.GetBytes(prefixString);
                string saltString = match.Groups["salt"].Value;
                saltBytes = Encoding.ASCII.GetBytes(saltString);
                formattedKey = FormatKey(password);
                truncatedSalt = ByteArray.TruncateAndCopy(saltBytes, 8);
                crypt = Crypt(formattedKey, truncatedSalt, prefixBytes, MD5.Create());
                string result = string.Concat(new object[]
                {
                    prefixString,
                    saltString,
                    '$',
                    Base64Encoding.UnixMD5.GetString(crypt)
                });
                result2 = result;
            }
            finally
            {
                Security.Clear(prefixBytes);
                Security.Clear(saltBytes);
                Security.Clear(formattedKey);
                Security.Clear(truncatedSalt);
                Security.Clear(crypt);
            }
            return result2;
        }

        /// <summary>
        /// Creates a one-way password hash (crypted password) from a password string and a salt string.
        ///
        /// The salt can be produced using <see cref="M:CryptSharp.Crypter.GenerateSalt(CryptSharp.CrypterOptions)" />.
        /// Because crypted passwords take the form <c>algorithm+salt+hash</c>, if you pass
        /// a crypted password as the salt parameter, the same algorithm and salt will be used to re-crypt the
        /// password. Since randomness comes from the salt, the same salt means the same hash, and so the
        /// same crypted password will result. Therefore, this method can both generate *and* verify crypted passwords.
        /// </summary>
        /// <param name="password">The password string. Characters are UTF-8 encoded.</param>
        /// <param name="salt">The salt string or crypted password containing a salt string.</param>
        /// <returns>The crypted password.</returns>
        public string Crypt(string password, string salt)
        {
            Check.Null("password", password);
            Check.Null("salt", salt);
            byte[] keyBytes = null;
            string result;
            try
            {
                keyBytes = Encoding.UTF8.GetBytes(password);
                result = Crypt(keyBytes, salt);
            }
            finally
            {
                Security.Clear(keyBytes);
            }
            return result;
        }

        private byte[] Crypt(byte[] key, byte[] salt, byte[] prefix, HashAlgorithm A)
        {
            byte[] H = null;
            byte[] I = null;
            byte[] result;
            try
            {
                A.Initialize();
                AddToDigest(A, key);
                AddToDigest(A, salt);
                AddToDigest(A, key);
                FinishDigest(A);
                I = (byte[])A.Hash.Clone();
                A.Initialize();
                AddToDigest(A, key);
                AddToDigest(A, prefix);
                AddToDigest(A, salt);
                AddToDigestRolling(A, I, 0, I.Length, key.Length);
                int length = key.Length;
                int i = 0;
                while (i < 31 && length != 0)
                {
                    AddToDigest(A, new byte[]
                    {
                        (length & 1 << i) != 0 ? default : key[0]
                    });
                    length &= ~(1 << i);
                    i++;
                }
                FinishDigest(A);
                H = (byte[])A.Hash.Clone();
                for (i = 0; i < 1000; i++)
                {
                    A.Initialize();
                    if ((i & 1) != 0)
                    {
                        AddToDigest(A, key);
                    }
                    if ((i & 1) == 0)
                    {
                        AddToDigest(A, H);
                    }
                    if (i % 3 != 0)
                    {
                        AddToDigest(A, salt);
                    }
                    if (i % 7 != 0)
                    {
                        AddToDigest(A, key);
                    }
                    if ((i & 1) != 0)
                    {
                        AddToDigest(A, H);
                    }
                    if ((i & 1) == 0)
                    {
                        AddToDigest(A, key);
                    }
                    FinishDigest(A);
                    Array.Copy(A.Hash, H, H.Length);
                }
                byte[] crypt = new byte[H.Length];
                int[] permutation = new int[]
                {
                    11,
                    4,
                    10,
                    5,
                    3,
                    9,
                    15,
                    2,
                    8,
                    14,
                    1,
                    7,
                    13,
                    0,
                    6,
                    12
                };
                Array.Reverse(permutation);
                for (i = 0; i < crypt.Length; i++)
                {
                    crypt[i] = H[permutation[i]];
                }
                result = crypt;
            }
            finally
            {
                A.Clear();
                Security.Clear(H);
                Security.Clear(I);
            }
            return result;
        }

        private void AddToDigest(HashAlgorithm algorithm, byte[] buffer)
        {
            AddToDigest(algorithm, buffer, 0, buffer.Length);
        }

        private void AddToDigest(HashAlgorithm algorithm, byte[] buffer, int offset, int count)
        {
            algorithm.TransformBlock(buffer, offset, count, buffer, offset);
        }

        private void AddToDigestRolling(HashAlgorithm algorithm, byte[] buffer, int offset, int inputCount, int outputCount)
        {
            for (int count = 0; count < outputCount; count += inputCount)
            {
                AddToDigest(algorithm, buffer, offset, Math.Min(outputCount - count, inputCount));
            }
        }

        private void FinishDigest(HashAlgorithm algorithm)
        {
            algorithm.TransformFinalBlock(new byte[0], 0, 0);
        }

        private byte[] FormatKey(byte[] key)
        {
            int length = ByteArray.NullTerminatedLength(key, key.Length);
            return ByteArray.TruncateAndCopy(key, length);
        }

        private static string Regex
        {
            get
            {
                return "\\A(?<prefix>\\$(apr)?1\\$)(?<salt>[A-Za-z0-9./]{1,99})(\\$(?<crypt>[A-Za-z0-9./]{22}))?\\z";
            }
        }

        // Token: 0x04000075 RID: 117
        private static readonly Regex _regex = new Regex(Regex, RegexOptions.CultureInvariant);
    }
}
