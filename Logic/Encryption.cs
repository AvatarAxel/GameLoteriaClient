using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
    }
}
