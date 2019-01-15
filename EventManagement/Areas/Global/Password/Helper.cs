using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace EventManagement.Areas.Global.Password
{
    public class Helper
    {
        public string GenerateRandomSalt()
        {
            return ComputeRandomSalt();
        }

        public string HashPasswordUsingSHA2(string Password, string Salt)
        {
            if (string.IsNullOrEmpty(Password))
            {
                return string.Empty;
            }

            ValidateClearTextPassword(Password);
            ValidateSalt(Salt);

            return ComputeSHA2Hash(Password, Salt);
        }


        public string ComputeRandomSalt()
        {
            string salt = string.Empty;

            for (int i = 0; i < 4; i++)
            {
                salt += Guid.NewGuid().ToString();
            }

            return salt.Replace("-", "").ToUpper();
        }

        internal string ComputeSHA2Hash(string ClearText, string Salt)
        {
            const string Pepper = "B0833AD1-3DDF-4FA3-A901-DEB0B766384D";

            Encoding utf8 = new UTF8Encoding();
            byte[] buffer = utf8.GetBytes(Salt + Pepper + ClearText + Pepper + Salt);

            // use the managed edition to avoid OS dependencies; slower but more future proof
            using (SHA512Managed sha = new SHA512Managed())
            {
                return BitConverter.ToString(sha.ComputeHash(buffer)).Replace("-", "");
            }
        }

        public void ValidateClearTextPassword(string Password)
        {
            if (string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException("The clear-text password cannot be blank.");
            }

            if (Password.Length > 16)
            {
                throw new ArgumentException("The clear-text password is too long.");
            }
        }

        internal void ValidateSalt(string Salt)
        {
            if (string.IsNullOrEmpty(Salt))
            {
                throw new ArgumentException("The salt cannot be empty.");
            }

            if (Salt.Length != 128)
            {
                throw new ArgumentException("The salt must be 128 characters.");
            }

            if (!new Regex(@"^[A-Za-z0-9]{128}$").IsMatch(Salt))
            {
                throw new ArgumentException("The salt contains invalid characters.");
            }
        }
    }
}