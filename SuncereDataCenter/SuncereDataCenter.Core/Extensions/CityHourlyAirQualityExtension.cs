using SuncereDataCenter.Core.Model;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.Extensions
{
    public static class CityHourlyAirQualityExtension
    {
        public static AirQualityShortTerm ToAirQualityShortTerm(this CityHourlyAirQuality source)
        {
            return new AirQualityShortTerm()
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
    }
}
