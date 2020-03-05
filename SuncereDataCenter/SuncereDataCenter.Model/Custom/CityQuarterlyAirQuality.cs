using SuncereDataCenter.Core.AirQuality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Model
{
    public partial class CityQuarterlyAirQuality : IAirQualityLongTerm
    {
        public double? SO2R { get; set; }
        public double? NO2R { get; set; }
        public double? PM10R { get; set; }
        public double? COR { get; set; }
        public double? O3R { get; set; }
        public double? PM25R { get; set; }
        public double? AQCIR { get; set; }
        public double? StandardDaysR { get; set; }
    }
}
