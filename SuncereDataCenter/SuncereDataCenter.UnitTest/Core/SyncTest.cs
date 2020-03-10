using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuncereDataCenter.Core.AirQuality;
using SuncereDataCenter.Core.Model;
using SuncereDataCenter.Core.Sync;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.UnitTest.Core
{
    [TestClass]
    public class SyncTest
    {
        [TestMethod]
        public void Test()
        {
            using (SuncereDataCenterEntities entities = new SuncereDataCenterEntities())
            {
                CityMonthlyAirQualitySync monthlySync = new CityMonthlyAirQualitySync(entities);
                CityQuarterlyAirQualitySync quarterlySync = new CityQuarterlyAirQualitySync(entities);
                CityYearlyAirQualitySync yearlySync = new CityYearlyAirQualitySync(entities);
                monthlySync.CheckQueue();
                quarterlySync.CheckQueue();
                yearlySync.CheckQueue();
                entities.SaveChanges();
                monthlySync.Sync();
                quarterlySync.Sync();
                yearlySync.Sync();
                entities.SaveChanges();
            }
        }
    }
}
