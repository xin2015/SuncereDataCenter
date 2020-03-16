using SuncereDataCenter.Basic.Calculators;
using SuncereDataCenter.Core.Extensions;
using SuncereDataCenter.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.AirQuality
{
    public class AirQualityLongTermYearOnYearCalculator
    {
        public List<AirQualityLongTermYearOnYear> Calculate(List<AirQualityLongTerm> list, List<AirQualityLongTerm> baseList)
        {
            List<AirQualityLongTermYearOnYear> newList = new List<AirQualityLongTermYearOnYear>();
            foreach (AirQualityLongTerm item in list)
            {
                AirQualityLongTermYearOnYear newItem = item.ToAirQualityLongTermYearOnYear();
                AirQualityLongTerm baseItem = baseList.FirstOrDefault(o => o.Code == item.Code);
                if (baseItem != null)
                {
                    if (item.AQCI.HasValue && baseItem.AQCI.HasValue)
                    {
                        newItem.AQCIYOY = Math.Round((item.AQCI.Value - baseItem.AQCI.Value) / baseItem.AQCI.Value * 100, 2);
                    }
                }
                newList.Add(newItem);
            }
            RankCalculator rankCalculator = new RankCalculator();
            rankCalculator.Calculate(newList, o => o.AQCIYOY, (o, rank) => { o.AQCIYOYR = rank; });
            return newList;
        }
    }
}
