using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SuncereDataCenter.Basic.CryptoTransverters;
using SuncereDataCenter.Core.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.UnitTest.Core
{
    [TestClass]
    public class TokenValidatorTest
    {
        [TestMethod]
        public void ValidateTest()
        {
            TokenModel tm = new TokenModel("admin", "123456");
            string token = AsymmetricEncryption.Default.EncryptToString(JsonConvert.SerializeObject(tm));
            bool result = TokenValidator.Validate(token, "", "");
            Assert.IsFalse(result);
            tm = new TokenModel("admin", "abc@123");
            token = AsymmetricEncryption.Default.EncryptToString(JsonConvert.SerializeObject(tm));
            result = TokenValidator.Validate(token, "", "");
            Assert.IsTrue(result);
            result = TokenValidator.Validate("123456", "", "");
            Assert.IsFalse(result);
        }
    }
}
