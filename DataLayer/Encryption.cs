using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class Encryption
    {
        public static string SHA512(string incomingPassword)
        {
            var bytes = Encoding.UTF8.GetBytes(incomingPassword);

            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new StringBuilder(128);

                foreach (var b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
