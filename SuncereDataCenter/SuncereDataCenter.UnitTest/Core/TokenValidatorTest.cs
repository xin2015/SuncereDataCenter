using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SuncereDataCenter.Basic.CryptoTransverters;
using SuncereDataCenter.Core.SystemManagement;
using SuncereDataCenter.Model;
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
            SuncereDataCenterModel entities = new SuncereDataCenterModel();
            TokenValidator validator = new TokenValidator(entities);
            TokenModel tm = new TokenModel("admin", "123456");
            string token = AsymmetricEncryption.Default.EncryptToString(JsonConvert.SerializeObject(tm));
            bool result = validator.Validate(token, "AirQuality", null);
            Assert.IsFalse(result);
            tm = new TokenModel("admin", "Suncere@123");
            token = AsymmetricEncryption.Default.EncryptToString(JsonConvert.SerializeObject(tm));
            result = validator.Validate(token, "AirQuality", null);
            Assert.IsTrue(result);
            result = validator.Validate(token, "AirQuality", "");
            Assert.IsFalse(result);
            result = validator.Validate("123456", "", "");
            Assert.IsFalse(result);
        }
    }
}
