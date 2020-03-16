using SuncereDataCenter.Core.Model;
using SuncereDataCenter.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuncereDataCenter.Basic.Calculators;

namespace SuncereDataCenter.Core.AirQuality
{
    public class AirQualityShortTermYearOnYearCalculator
    {
        public List<AirQualityShortTermYearOnYear> Calculate(List<AirQualityShortTerm> list, List<AirQualityShortTerm> baseList)
        {
            List<AirQualityShortTermYearOnYear> newList = new List<AirQualityShortTermYearOnYear>();
            foreach (AirQualityShortTerm item in list)
            {
                AirQualityShortTermYearOnYear newItem = item.ToAirQualityShortTermYearOnYear();
                AirQualityShortTerm baseItem = baseList.FirstOrDefault(o => o.Code == item.Code);
                if (baseItem != null)
                {
                    if (item.AQI.HasValue && baseItem.AQI.HasValue)
                    {
                        newItem.AQIYOY = Math.Round((item.AQI.Value - baseItem.AQI.Value) / baseItem.AQI.Value * 100, 2);
                    }
                }
                newList.Add(newItem);
            }
            RankCalculator rankCalculator = new RankCalculator();
            rankCalculator.Calculate(newList, o => o.AQIYOY, (o, rank) => { o.AQIYOYR = rank; });
            return newList;
        }
    }
}
