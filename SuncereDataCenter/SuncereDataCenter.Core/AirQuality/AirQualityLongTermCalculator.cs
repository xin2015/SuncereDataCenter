using SuncereDataCenter.Basic.Calculators;
using SuncereDataCenter.Basic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.AirQuality
{
    public class AirQualityLongTermCalculator
    {
        public void CalculateRank(IEnumerable<IAirQualityLongTerm> data)
        {
            RankCalculator rankCalculator = new RankCalculator();
            rankCalculator.Calculate(data, o => o.SO2, (o, rank) => { o.SO2R = rank; });
            rankCalculator.Calculate(data, o => o.NO2, (o, rank) => { o.NO2R = rank; });
            rankCalculator.Calculate(data, o => o.PM10, (o, rank) => { o.PM10R = rank; });
            rankCalculator.Calculate(data, o => o.CO, (o, rank) => { o.COR = rank; });
            rankCalculator.Calculate(data, o => o.O3, (o, rank) => { o.O3R = rank; });
            rankCalculator.Calculate(data, o => o.PM25, (o, rank) => { o.PM25R = rank; });
            rankCalculator.Calculate(data, o => o.AQCI, (o, rank) => { o.AQCIR = rank; });
            rankCalculator.Calculate(data, o => o.StandardDays, (o, rank) => { o.StandardDaysR = rank; });
        }

        public void Calculate(IEnumerable<IAirQualityIndexCalculate> data, IAirQualityStatistics result)
        {
            result.SO2 = data.Average(o => o.SO2);
            if (result.SO2.HasValue)
            {
                result.SO2 = Math.Round(result.SO2.Value);
            }
            result.NO2 = data.Average(o => o.NO2);
            if (result.NO2.HasValue)
            {
                result.NO2 = Math.Round(result.NO2.Value);
            }
            result.PM10 = data.Average(o => o.PM10);
            if (result.PM10.HasValue)
            {
                result.PM10 = Math.Round(result.PM10.Value);
            }
            result.CO = data.Percentile(o => o.CO, 0.95);
            if (result.CO.HasValue)
            {
                result.CO = Math.Round(result.CO.Value, 1);
            }
            result.O3 = data.Percentile(o => o.O3, 0.9);
            if (result.O3.HasValue)
            {
                result.O3 = Math.Round(result.O3.Value);
            }
            result.PM25 = data.Average(o => o.PM25);
            if (result.PM25.HasValue)
            {
                result.PM25 = Math.Round(result.PM25.Value);
            }
            result.StandardDays = data.Count(o => o.AQI.HasValue && o.AQI <= 100);
            AirQualityCompositeIndexCalculator calculator = new AirQualityCompositeIndexCalculator();
            calculator.CheckIntegrity = true;
            calculator.CalculateAirQualityCompositeIndex(result);
        }
    }
}
