using SuncereDataCenter.Core.Model;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.Extensions
{
    public static class AirQualityShortTermExtension
    {
        public static CityHourlyAirQuality ToCityHourlyAirQuality(this AirQualityShortTerm source)
        {
            return new CityHourlyAirQuality()
            {
                Code = source.Code,
                Time = source.Time,
                Name = source.Name,
                SO2 = source.SO2,
                NO2 = source.NO2,
                PM10 = source.PM10,
                CO = source.CO,
                O3 = source.O3,
                PM25 = source.PM25,
                AQI = source.AQI,
                Type = source.Type,
                PrimaryPollutant = source.PrimaryPollutant
            };
        }

        public static CityDailyAirQuality ToCityDailyAirQuality(this AirQualityShortTerm source)
        {
            return new CityDailyAirQuality()
            {
                Code = source.Code,
                Time = source.Time,
                Name = source.Name,
                SO2 = source.SO2,
                NO2 = source.NO2,
                PM10 = source.PM10,
                CO = source.CO,
                O3 = source.O3,
                PM25 = source.PM25,
                AQI = source.AQI,
                Type = source.Type,
                PrimaryPollutant = source.PrimaryPollutant
            };
        }

        public static AirQualityShortTermYearOnYear ToAirQualityShortTermYearOnYear(this AirQualityShortTerm source)
        {
            return new AirQualityShortTermYearOnYear()
            {
                Code = source.Code,
                Time = source.Time,
                Name = source.Name,
                SO2 = source.SO2,
                NO2 = source.NO2,
                PM10 = source.PM10,
                CO = source.CO,
                O3 = source.O3,
                PM25 = source.PM25,
                AQI = source.AQI,
                Type = source.Type,
                PrimaryPollutant = source.PrimaryPollutant,
                SO2R = source.SO2R,
                NO2R = source.NO2R,
                PM10R = source.PM10R,
                COR = source.COR,
                O3R = source.O3R,
                PM25R = source.PM25R,
                AQIR = source.AQIR
            };
        }
    }
}
