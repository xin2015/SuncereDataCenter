using SuncereDataCenter.Core.Model;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.Extensions
{
    public static class CityQuarterlyAirQualityExtension
    {
        public static AirQualityLongTerm ToAirQualityLongTerm(this CityQuarterlyAirQuality source)
        {
            return new AirQualityLongTerm()
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
    }
}
