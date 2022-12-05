using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic
{
    public class FieldValidation
    {
        public bool PasswordValidation(string password, string ValidatePassword)
        {
            if (password.Equals(ValidatePassword)) 
            { 
                return true;
            }
            return false;
        }
        public bool ValidationEmailFormat(String email)
        {
            if (Regex.IsMatch(email, "^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@(([a-zA-Z]+[\\w-]+\\.){1,2}[a-zA-Z]{2,4})$")) 
            {
                return true;
            }
            return false;
        }

        public bool ValidationUsernameFormat(string username)
        {
            if ((Regex.IsMatch(username, @"^[^ ][a-zA-Z 0-9]+[^ ]$")))
            {
                return true;
            }
            return false;
        }

        public bool PasswordSecure(string password)
        {
            Regex numbers = new Regex(@"[0-9]");
            bool normals = false;
            bool number = false;
            bool mayus = false;
            if (numbers.IsMatch(password))
            {
                number = true;
            }
            if (password.Length > 8)
            {
                for (int i = 0; i < password.Length; i++)
                {
                    if (Char.IsLower(password, i))
                    {
                        normals = true;
                    }
                    else if (Char.IsUpper(password, i))
                    {
                        mayus = true;
                    }

                }
                if (normals || mayus || number)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
