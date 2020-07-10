using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FbiInquiry.Web.Areas.FbiToken
{
    public static class FbiTokenTools
    {
        #region Md5Hash
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        #endregion

        #region TokenIsValid
        public static bool TokenIsValid(string Key, string Token)
        {
            try
            {
                string Password;
                var epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds.ToString().Substring(0, 8);
                string TimeSpn = Convert.ToInt64(epoch).ToString().Substring(0, 8);
                Int64 timecode = (Convert.ToInt64(TimeSpn) * 19) + 931;

                using (MD5 md5Hash = MD5.Create())
                {
                    string MD5TimeSpn = GetMd5Hash(md5Hash, timecode.ToString());
                    string MD5Key = GetMd5Hash(md5Hash, Key);
                    Password = GetMd5Hash(md5Hash, MD5TimeSpn + MD5Key);
                }

                if (Token == Password)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

        public static string GetToken(string Key)
        {
            try
            {
                string Password;
                var epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds.ToString().Substring(0, 8);
                string TimeSpn = Convert.ToInt64(epoch).ToString().Substring(0, 8);
                Int64 timecode = (Convert.ToInt64(TimeSpn) * 19) + 931;

                using (MD5 md5Hash = MD5.Create())
                {
                    string MD5TimeSpn = GetMd5Hash(md5Hash, timecode.ToString());
                    string MD5Key = GetMd5Hash(md5Hash, Key);
                    Password = GetMd5Hash(md5Hash, MD5TimeSpn + MD5Key);
                }

                return Password;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
