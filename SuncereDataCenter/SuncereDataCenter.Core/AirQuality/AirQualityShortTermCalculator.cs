using SuncereDataCenter.Basic.Calculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.AirQuality
{
    public class AirQualityShortTermCalculator
    {
        public void CalculateRank(IEnumerable<IAirQualityShortTerm> data)
        {
            RankCalculator rankCalculator = new RankCalculator();
            rankCalculator.Calculate(data, o => o.SO2, (o, rank) => { o.SO2R = rank; });
            rankCalculator.Calculate(data, o => o.NO2, (o, rank) => { o.NO2R = rank; });
            rankCalculator.Calculate(data, o => o.PM10, (o, rank) => { o.PM10R = rank; });
            rankCalculator.Calculate(data, o => o.CO, (o, rank) => { o.COR = rank; });
            rankCalculator.Calculate(data, o => o.O3, (o, rank) => { o.O3R = rank; });
            rankCalculator.Calculate(data, o => o.PM25, (o, rank) => { o.PM25R = rank; });
            rankCalculator.Calculate(data, o => o.AQI, (o, rank) => { o.AQIR = rank; });
        }
    }
}
