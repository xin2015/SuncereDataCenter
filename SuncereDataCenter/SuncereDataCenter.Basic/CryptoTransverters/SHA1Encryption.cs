using SuncereDataCenter.Basic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Basic.CryptoTransverters
{
    public class SHA1Encryption : IEncrypt
    {
        public static SHA1Encryption Default { get; set; }

        public static string PasswordSalt { get; set; }

        static SHA1Encryption()
        {
            Default = new SHA1Encryption();
            PasswordSalt = "Suncere";
        }

        protected SHA1 SHA { get; set; }

        public SHA1Encryption()
        {
            SHA = SHA1.Create();
        }

        public byte[] EncryptToBytes(byte[] inputBuffer)
        {
            return SHA.ComputeHash(inputBuffer);
        }

        public byte[] EncryptToBytes(string inString)
        {
            byte[] inputBuffer = inString.FromUTF8String();
            return EncryptToBytes(inputBuffer);
        }

        public string EncryptToString(byte[] inputBuffer)
        {
            return EncryptToBytes(inputBuffer).ToBase64String();
        }

        public string EncryptToString(string inString)
        {
            byte[] inputBuffer = inString.FromUTF8String();
            return EncryptToString(inputBuffer);
        }

        public string EncryptPassword(string password)
        {
            return EncryptToString(password + PasswordSalt);
        }
    }
}
