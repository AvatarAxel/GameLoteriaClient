using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CodeGame
    {
        public string GenerateRandomCode()
        {
            var random = new Random();
            var value = random.Next(0, 10000);

            string verificationCode = value.ToString();

            return verificationCode;
        }
    }
}
