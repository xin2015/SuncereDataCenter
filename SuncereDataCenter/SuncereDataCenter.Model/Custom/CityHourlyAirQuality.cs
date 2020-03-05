using SuncereDataCenter.Core.AirQuality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Model
{
    public partial class CityHourlyAirQuality: IAirQualityShortTerm
    {
        public double? SO2R { get; set; }
        public double? NO2R { get; set; }
        public double? PM10R { get; set; }
        public double? COR { get; set; }
        public double? O3R { get; set; }
        public double? PM25R { get; set; }
        public double? AQIR { get; set; }
    }
}
