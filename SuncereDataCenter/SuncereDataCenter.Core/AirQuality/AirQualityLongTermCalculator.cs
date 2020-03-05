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
        public void CalculatorRank(IEnumerable<IAirQualityLongTerm> data)
        {
            RankCalculator rankCalculator = new RankCalculator();
            rankCalculator.Calculate<IAirQualityLongTerm>(data, o => o.SO2, (o, rank) => { o.SO2R = rank; });
            rankCalculator.Calculate<IAirQualityLongTerm>(data, o => o.NO2, (o, rank) => { o.NO2R = rank; });
            rankCalculator.Calculate<IAirQualityLongTerm>(data, o => o.PM10, (o, rank) => { o.PM10R = rank; });
            rankCalculator.Calculate<IAirQualityLongTerm>(data, o => o.CO, (o, rank) => { o.COR = rank; });
            rankCalculator.Calculate<IAirQualityLongTerm>(data, o => o.O3, (o, rank) => { o.O3R = rank; });
            rankCalculator.Calculate<IAirQualityLongTerm>(data, o => o.PM25, (o, rank) => { o.PM25R = rank; });
            rankCalculator.Calculate<IAirQualityLongTerm>(data, o => o.AQCI, (o, rank) => { o.AQCIR = rank; });
            rankCalculator.Calculate<IAirQualityLongTerm>(data, o => o.StandardDays, (o, rank) => { o.StandardDaysR = rank; });
        }

        public void Calculate(IEnumerable<IAirQualityIndexCalculate> data, IAirQualityStatistics result)
        {
            result.SO2 = data.Average(o => o.SO2);
            result.NO2 = data.Average(o => o.NO2);
            result.PM10 = data.Average(o => o.PM10);
            result.CO = data.Percentile(o => o.CO, 0.95);
            result.O3 = data.Percentile(o => o.O3, 0.9);
            result.PM25 = data.Average(o => o.PM25);
            result.StandardDays = data.Count(o => o.AQI.HasValue && o.AQI <= 100);
            AirQualityCompositeIndexCalculator calculator = new AirQualityCompositeIndexCalculator();
            calculator.CheckIntegrity = true;
            calculator.CalculateAirQualityCompositeIndex(result);
        }
    }
}
