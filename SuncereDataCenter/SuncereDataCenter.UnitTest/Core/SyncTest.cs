using AutoMapper;
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
            DateTime time = new DateTime(2020, 2, 22);
            using (SuncereDataCenterEntities entities = new SuncereDataCenterEntities())
            {
                CityMonthlyAirQualitySync monthlySync = new CityMonthlyAirQualitySync(entities);
                //CityQuarterlyAirQualitySync quarterlySync = new CityQuarterlyAirQualitySync(entities);
                //CityYearlyAirQualitySync yearlySync = new CityYearlyAirQualitySync(entities);
                monthlySync.CheckQueue(time);
                entities.SaveChanges();
                monthlySync.Sync();
                entities.SaveChanges();
            }

        }


        [TestMethod]
        public void AutoMapperTest()
        {
            DateTime time = DateTime.Today;
            DateTime startTime = new DateTime(time.Year, time.Month, 1);
            DateTime endTime = startTime.AddMonths(1).AddDays(-1);
            Mapper.Initialize(x => x.CreateMap<CityDailyAirQuality, TestAirQuality>());
            using (SuncereDataCenterEntities entities = new SuncereDataCenterEntities())
            {
                List<CityDailyAirQuality> source = entities.CityDailyAirQuality.Where(o => o.Time >= startTime && o.Time <= endTime).ToList();
                List<TestAirQuality> list = Mapper.Map<List<TestAirQuality>>(source);
            }
        }

        [TestMethod]
        public void TestSync2()
        {
            DateTime time = DateTime.Today;
            DateTime startTime = new DateTime(time.Year, time.Month, 1);
            DateTime endTime = startTime.AddMonths(1).AddDays(-1);
            SuncereDataCenterEntities Entities = new SuncereDataCenterEntities();
            List<CityDailyAirQuality> source = Entities.CityDailyAirQuality.Where(o => o.Time >= startTime && o.Time <= endTime).ToList();
            if (source.Any())
            {
                Mapper.Initialize(x => x.CreateMap<CityDailyAirQuality, TestAirQuality>());
                List<TestAirQuality> airQualityShortTermList = Mapper.Map<List<TestAirQuality>>(source);
                List<CityMonthlyAirQuality> list = new List<CityMonthlyAirQuality>();
                Mapper.Initialize(x => x.CreateMap<CityMonthlyAirQuality, AirQualityLongTerm>());
                AirQualityLongTermCalculator calculator = new AirQualityLongTermCalculator();
                foreach (var cityGroup in airQualityShortTermList.GroupBy(o => o.Code))
                {
                    TestAirQuality first = cityGroup.First();
                    AirQualityLongTerm item = new AirQualityLongTerm()
                    {
                        Code = first.Code,
                        Time = startTime,
                        Name = first.Name
                    };
                    calculator.Calculate(cityGroup, item);
                    list.Add(Mapper.Map<CityMonthlyAirQuality>(item));
                }
                IQueryable<CityMonthlyAirQuality> oldList = Entities.CityMonthlyAirQuality.Where(o => o.Time == startTime);
                Entities.CityMonthlyAirQuality.RemoveRange(oldList);
                Entities.CityMonthlyAirQuality.AddRange(list);
            }
            Entities.Dispose();
        }
    }

    public class TestAirQuality : IAirQualityShortTerm
    {
        public string Code { get; set; }
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public double? SO2 { get; set; }
        public double? NO2 { get; set; }
        public double? PM10 { get; set; }
        public double? CO { get; set; }
        public double? O3 { get; set; }
        public double? PM25 { get; set; }
        public double? AQI { get; set; }
        public string Type { get; set; }
        public string PrimaryPollutant { get; set; }
        public double? SO2R { get; set; }
        public double? NO2R { get; set; }
        public double? PM10R { get; set; }
        public double? COR { get; set; }
        public double? O3R { get; set; }
        public double? PM25R { get; set; }
        public double? AQIR { get; set; }
    }
}
