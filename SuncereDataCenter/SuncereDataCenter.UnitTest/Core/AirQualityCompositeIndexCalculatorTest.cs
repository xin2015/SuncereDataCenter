using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuncereDataCenter.Core.AirQuality;

namespace SuncereDataCenter.UnitTest
{
    [TestClass]
    public class AirQualityCompositeIndexCalculatorTest
    {
        [TestMethod]
        public void CalculateAirQualityCompositeIndexTest()
        {
            AirQualityCompositeIndexCalculator calculator = new AirQualityCompositeIndexCalculator();
            calculator.CheckIntegrity = true;
            AirQualityCompositeIndexCalculate data1 = new AirQualityCompositeIndexCalculate()
            {
                SO2 = 12,
                NO2 = 12,
                PM10 = 12,
                PM25 = 12,
                CO = null,
                O3 = 17
            };
            calculator.CalculateAirQualityCompositeIndex(data1);
            Assert.IsNull(data1.AQCI);
            Assert.IsNull(data1.PrimaryPollutant);
            AirQualityCompositeIndexCalculate data2 = new AirQualityCompositeIndexCalculate()
            {
                SO2 = 21,
                NO2 = 21,
                PM10 = 21,
                PM25 = 21,
                CO = 0.1,
                O3 = 34
            };
            calculator.CalculateAirQualityCompositeIndex(data2);
            Assert.AreEqual(data2.AQCI, 2);
            Assert.AreEqual(data2.PrimaryPollutant, "PM25");
            AirQualityCompositeIndexCalculate data3 = new AirQualityCompositeIndexCalculate()
            {
                SO2 = 14,
                NO2 = 14,
                PM10 = 14,
                PM25 = 14,
                CO = 0.2,
                O3 = 18
            };
            calculator.CalculateAirQualityCompositeIndex(data3);
            Assert.AreEqual(data3.AQCI, 1.34);
            Assert.AreEqual(data3.PrimaryPollutant, "PM25");
        }
    }
}
