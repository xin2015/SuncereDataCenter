using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuncereDataCenter.Core.AirQuality;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.UnitTest.Model
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void Test()
        {
            Random rand = new Random();
            AirQualityCompositeIndexCalculator calculator = new AirQualityCompositeIndexCalculator();
            calculator.CheckIntegrity = true;
            List<MonthlyAirQuality> list = new List<MonthlyAirQuality>();
            for (int i = 0; i < 100; i++)
            {
                MonthlyAirQuality item = new MonthlyAirQuality()
                {
                    Code = string.Format("Code{0}", i.ToString().PadLeft(3, '0')),
                    Time = DateTime.Today,
                    Name = string.Format("Name{0}", i.ToString().PadLeft(3, '0')),
                    SO2 = Math.Round(rand.NextDouble() * 60),
                    NO2 = Math.Round(rand.NextDouble() * 40),
                    PM10 = Math.Round(rand.NextDouble() * 70),
                    CO = Math.Round(rand.NextDouble() * 4, 1),
                    O3 = Math.Round(rand.NextDouble() * 160),
                    PM25 = Math.Round(rand.NextDouble() * 35),
                    StandardDays = rand.Next(30)
                };
                calculator.CalculateAirQualityCompositeIndex(item);
                list.Add(item);
            }
            using (SuncereDataCenterEntities db = new SuncereDataCenterEntities())
            {
                db.MonthlyAirQuality.AddRange(list);
                db.SaveChanges();
            }
        }
    }
}
