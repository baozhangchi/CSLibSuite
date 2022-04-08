using SVNUtils.CryptSharp;
using System.Collections.Generic;

namespace SVNUtils
{
    internal static class PasswordHelper
    {
        public static string Crypt(string password)
        {
            return new MD5Crypter().Crypt(password, new CrypterOptions()
            {
                { CrypterOption.Variant, MD5CrypterVariant.Apache }
            });
        }

        public static bool CheckPassword(string password, string cryptedPassword)
        {
            return EqualityComparer<string>.Default.Equals(Crypt(password), cryptedPassword);
        }
    }
}
