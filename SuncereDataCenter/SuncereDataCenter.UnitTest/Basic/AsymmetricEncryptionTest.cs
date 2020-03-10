using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuncereDataCenter.Basic.CryptoTransverters;
using SuncereDataCenter.Basic.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.UnitTest.Basic
{
    [TestClass]
    public class AsymmetricEncryptionTest
    {
        [TestMethod]
        public void Test()
        {
            Random rand = new Random();
            AsymmetricEncryption ae = new AsymmetricEncryption();
            for (int i = 0; i < 100; i++)
            {
                byte[] data = new byte[10000];
                rand.NextBytes(data);
                string text = data.ToBase64String();
                string ciphertext = ae.EncryptToString(text);
                string plaintext = ae.DecryptToString(ciphertext);
                Assert.AreEqual(text, plaintext);
            }
        }

        [TestMethod]
        public void AsymmetricEncryptionDefaultTest()
        {
            Random rand = new Random();
            byte[] data = new byte[10000];
            rand.NextBytes(data);
            string text = data.ToBase64String();
            string ciphertext = AsymmetricEncryption.Default.EncryptToString(text);
            string plaintext = AsymmetricEncryption.Default.DecryptToString(ciphertext);
            Assert.AreEqual(text, plaintext);
        }

        [TestMethod]
        public void PasswordEncrypt()
        {
            string password = "Suncere@123";
            string ciphertext = SHA1Encryption.Default.EncryptPassword(password);
        }
    }
}
