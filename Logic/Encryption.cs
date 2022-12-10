using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace Logic
{
    public class Encryption
    {
        public string HashPassword256(string password) 
        {
            using (SHA256 sha256 = SHA256.Create()) 
            {
                string hashedPassword;
                Byte[] passwordHashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < passwordHashedBytes.Length; i++)
                {
                    stringBuilder.Append(passwordHashedBytes[i].ToString("x2"));
                }
                hashedPassword = stringBuilder.ToString();
                return hashedPassword;
            }
        }
        public string EncryptionMessage(string message)
        {
            string hash = (Properties.Settings.Default.HASH);
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(message);

            MD5 md5 = MD5.Create();
            TripleDES tripledes = TripleDES.Create();

            tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripledes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripledes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(bytes, 0, bytes.Length);

            return Convert.ToBase64String(result);
        }

        public string DescryptionMessage(string messageEncryptation)
        {
            string hash = (Properties.Settings.Default.HASH);
            byte[] bytes = Convert.FromBase64String(messageEncryptation);

            MD5 md5 = MD5.Create();
            TripleDES tripledes = TripleDES.Create();

            tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripledes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripledes.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(bytes, 0, bytes.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
}
