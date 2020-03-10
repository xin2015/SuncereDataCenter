using SuncereDataCenter.Basic.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Basic.CryptoTransverters
{
    /// <summary>
    /// 非对称加密
    /// </summary>
    public class AsymmetricEncryption : IEncrypt, IDecrypt
    {
        public static AsymmetricEncryption Default { get; set; }
        static AsymmetricEncryption()
        {
            Default = new AsymmetricEncryption();
            Default.FromXmlString(File.ReadAllText(ConfigurationManager.AppSettings["RSAKeyPath"]));
        }

        protected RSACryptoServiceProvider RSA { get; set; }
        protected int DecryptCount { get; set; }
        protected int EncryptCount { get; set; }

        public AsymmetricEncryption()
        {
            RSA = new RSACryptoServiceProvider();
            DecryptCount = RSA.KeySize / 8;
        }

        #region Encrypt
        public byte[] EncryptToBytes(byte[] inputBuffer, bool fOAEP)
        {
            EncryptCount = fOAEP ? DecryptCount - 42 : DecryptCount - 11;
            if (inputBuffer.Length <= EncryptCount)
            {
                return RSA.Encrypt(inputBuffer, fOAEP);
            }
            else
            {
                List<byte> list = new List<byte>();
                MemoryStream ms = new MemoryStream(inputBuffer);
                int count = 0;
                while (count < inputBuffer.Length)
                {
                    byte[] buffer = new byte[inputBuffer.Length - count < EncryptCount ? inputBuffer.Length - count : EncryptCount];
                    count += ms.Read(buffer, 0, buffer.Length);
                    list.AddRange(RSA.Encrypt(buffer, fOAEP));
                }
                return list.ToArray();
            }
        }

        public byte[] EncryptToBytes(string inString, bool fOAEP)
        {
            byte[] inputBuffer = inString.FromUTF8String();
            return EncryptToBytes(inputBuffer, fOAEP);
        }

        public string EncryptToString(byte[] inputBuffer, bool fOAEP)
        {
            return EncryptToBytes(inputBuffer, fOAEP).ToBase64String();
        }

        public string EncryptToString(string inString, bool fOAEP)
        {
            byte[] inputBuffer = inString.FromUTF8String();
            return EncryptToString(inputBuffer, fOAEP);
        }

        public byte[] EncryptToBytes(byte[] inputBuffer)
        {
            return EncryptToBytes(inputBuffer, true);
        }

        public byte[] EncryptToBytes(string inString)
        {
            return EncryptToBytes(inString, true);
        }

        public string EncryptToString(byte[] inputBuffer)
        {
            return EncryptToString(inputBuffer, true);
        }

        public string EncryptToString(string inString)
        {
            return EncryptToString(inString, true);
        }
        #endregion
        #region Decrypt
        public byte[] DecryptToBytes(byte[] inputBuffer, bool fOAEP)
        {
            if (inputBuffer.Length <= DecryptCount)
            {
                return RSA.Decrypt(inputBuffer, fOAEP);
            }
            else
            {
                List<byte> list = new List<byte>();
                MemoryStream ms = new MemoryStream(inputBuffer);
                int count = 0;
                while (count < inputBuffer.Length)
                {
                    byte[] buffer = new byte[inputBuffer.Length - count < DecryptCount ? inputBuffer.Length - count : DecryptCount];
                    count += ms.Read(buffer, 0, buffer.Length);
                    list.AddRange(RSA.Decrypt(buffer, fOAEP));
                }
                return list.ToArray();
            }
        }

        public byte[] DecryptToBytes(string inString, bool fOAEP)
        {
            byte[] inputBuffer = inString.FromBase64String();
            return DecryptToBytes(inputBuffer, fOAEP);
        }

        public string DecryptToString(byte[] inputBuffer, bool fOAEP)
        {
            return DecryptToBytes(inputBuffer, fOAEP).ToUTF8String();
        }

        public string DecryptToString(string inString, bool fOAEP)
        {
            byte[] inputBuffer = inString.FromBase64String();
            return DecryptToString(inputBuffer, fOAEP);
        }

        public byte[] DecryptToBytes(byte[] inputBuffer)
        {
            return DecryptToBytes(inputBuffer, true);
        }

        public byte[] DecryptToBytes(string inString)
        {
            return DecryptToBytes(inString, true);
        }

        public string DecryptToString(byte[] inputBuffer)
        {
            return DecryptToString(inputBuffer, true);
        }

        public string DecryptToString(string inString)
        {
            return DecryptToString(inString, true);
        }
        #endregion
        #region Key
        public RSAParameters ExportParameters(bool includePrivateParameters)
        {
            return RSA.ExportParameters(includePrivateParameters);
        }

        public void ImportParameters(RSAParameters parameters)
        {
            RSA.ImportParameters(parameters);
        }

        public string ToXmlString(bool includePrivateParameters)
        {
            return RSA.ToXmlString(includePrivateParameters);
        }

        public void FromXmlString(string xmlString)
        {
            RSA.FromXmlString(xmlString);
        }
        #endregion
    }
}
