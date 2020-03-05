using SuncereDataCenter.Core.AirQuality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.AirQuality
{
    public interface IAirQualityShortTerm : IAirQualityIndexCalculate
    {
        double? SO2R { get; set; }
        double? NO2R { get; set; }
        double? PM10R { get; set; }
        double? COR { get; set; }
        double? O3R { get; set; }
        double? PM25R { get; set; }
        double? AQIR { get; set; }
    }
}
