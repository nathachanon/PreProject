using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MASdemo.Security
{
    public class Encryption
    {
        public static string EncryptedPass(string input)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] b = Encoding.UTF8.GetBytes(input);
                b = md5.ComputeHash(b);
                StringBuilder sb = new StringBuilder();
                foreach (byte x in b)
                    sb.Append(x.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
