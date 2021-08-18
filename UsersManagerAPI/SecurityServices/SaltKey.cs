using System.Security.Cryptography;
using System.Text;

namespace UsersManagerAPI.SecurityServices
{
    public class SaltKey
    {
        //WIP
        //creates random salt key to be added to password before hashing
        public string GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < salt.Length; i++)
            {
                builder.Append(salt[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
