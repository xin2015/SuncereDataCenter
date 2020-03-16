using SuncereDataCenter.Core.Model;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.Extensions
{
    public static class AirQualityLongTermExtension
    {
        public static CityMonthlyAirQuality ToCityMonthlyAirQuality(this AirQualityLongTerm source)
        {
            return new CityMonthlyAirQuality()
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
                AQCI = source.AQCI,
                StandardDays = source.StandardDays,
                PrimaryPollutant = source.PrimaryPollutant
            };
        }

        public static CityQuarterlyAirQuality ToCityQuarterlyAirQuality(this AirQualityLongTerm source)
        {
            return new CityQuarterlyAirQuality()
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
                AQCI = source.AQCI,
                StandardDays = source.StandardDays,
                PrimaryPollutant = source.PrimaryPollutant
            };
        }

        public static CityYearlyAirQuality ToCityYearlyAirQuality(this AirQualityLongTerm source)
        {
            return new CityYearlyAirQuality()
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
                AQCI = source.AQCI,
                StandardDays = source.StandardDays,
                PrimaryPollutant = source.PrimaryPollutant
            };
        }

        public static AirQualityLongTermYearOnYear ToAirQualityLongTermYearOnYear(this AirQualityLongTerm source)
        {
            return new AirQualityLongTermYearOnYear()
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
                AQCI = source.AQCI,
                StandardDays = source.StandardDays,
                PrimaryPollutant = source.PrimaryPollutant,
                SO2R = source.SO2R,
                NO2R = source.NO2R,
                PM10R = source.PM10R,
                COR = source.COR,
                O3R = source.O3R,
                PM25R = source.PM25R,
                AQCIR = source.AQCIR,
                StandardDaysR = source.StandardDaysR
            };
        }
    }
}
