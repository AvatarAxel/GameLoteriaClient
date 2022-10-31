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
    }
}
