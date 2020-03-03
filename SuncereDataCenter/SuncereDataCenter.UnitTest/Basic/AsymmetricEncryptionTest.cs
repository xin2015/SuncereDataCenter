using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuncereDataCenter.Basic.CryptoTransverters;
using SuncereDataCenter.Basic.Extensions;
using System;
using System.Collections.Generic;
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
        public void CalculateAirQualityCompositeIndexTest()
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
        public void RSACryptoServiceProviderTest()
        {
            Random rand = new Random();
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);
            for (int i = 213; i < 100000; i++)
            {
                byte[] inputBuffer = new byte[i];
                rand.NextBytes(inputBuffer);
                byte[] outputBuffer = RSA.Encrypt(inputBuffer, true);
            }
        }
    }
}
