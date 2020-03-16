using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SuncereDataCenter.Basic.CryptoTransverters;
using SuncereDataCenter.Core.SystemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SuncereDataCenter.UnitTest.API
{
    [TestClass]
    public class CityAirQualityTest
    {
        [TestMethod]
        public void GetCityHourlyAirQualityTest()
        {
            using (WebClient wc = new WebClient())
            {
                TokenModel tm = new TokenModel("admin", "Suncere@123");
                tm.Time = new DateTime(2222, 1, 1);
                //TokenModel tm = new TokenModel("test", "Test@2020");
                string token = AsymmetricEncryption.Default.EncryptToString(JsonConvert.SerializeObject(tm));
                token = HttpUtility.UrlEncode(token);
                string url = string.Format("http://cityphoto.suncereltd.cn:8082/api/CityAirQuality/GetCityAnyTimeRangeAirQuality?token={0}&startTime={1}&endTime={2}&areaCode={3}", token, "2020-03-01", "2020-03-09", "");
                string result = wc.DownloadString(url);
            }
        }
    }
}
