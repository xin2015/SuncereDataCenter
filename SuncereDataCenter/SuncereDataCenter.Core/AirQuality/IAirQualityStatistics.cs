using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.AirQuality
{
    public interface IAirQualityStatistics: IAirQualityCompositeIndexCalculate
    {
        double? StandardDays { get; set; }
    }
}
