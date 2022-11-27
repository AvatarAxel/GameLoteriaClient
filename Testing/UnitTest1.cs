using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        private FieldValidation fieldValidation = new FieldValidation();
        [TestMethod]
        public void PasswordValidation_Successful()
        {
            bool expectedResult = true;
            bool actualResult;

            string password = "1234";
            string validationPassword = "1234";

            actualResult = fieldValidation.PasswordValidation(password, validationPassword);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void PasswordValidation_Unsuccessful()
        {
            bool expectedResult = false;
            bool actualResult;

            string password = "1234";
            string validationPassword = "4321";

            actualResult = fieldValidation.PasswordValidation(password, validationPassword);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ValidationEmailFormat_Successful()
        {
            bool expectedResult = true;
            bool actualResult;

            string email = "aavp1603@hotmail.com"; 

            actualResult = fieldValidation.ValidationEmailFormat(email);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ValidationEmailFormat_Unsuccessful()
        {
            bool expectedResult = false;
            bool actualResult;

            string email = "123cq.jdyci@jcynadocon.jcy wofue sudyc";

            actualResult = fieldValidation.ValidationEmailFormat(email);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ValidationUsernameFormat_Successful()
        {
            bool expectedResult = true;
            bool actualResult;

            string username = "Alex454";

            actualResult = fieldValidation.ValidationUsernameFormat(username);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ValidationUsernameFormat_Unsuccessful()
        {
            bool expectedResult = false;
            bool actualResult;

            string username = "🤣@@@___{{+{😍😍";

            actualResult = fieldValidation.ValidationUsernameFormat(username);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void PasswordSecure_Unsuccessful()
        {
            bool expectedResult = false;
            bool actualResult;

            string password = "1234";

            actualResult = fieldValidation.PasswordSecure(password);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void PasswordSecure_Successful()
        {
            bool expectedResult = true;
            bool actualResult;

            string password = "AlYUBw32e987";

            actualResult = fieldValidation.PasswordSecure(password);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
    
}
