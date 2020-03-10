using SuncereDataCenter.Core.AirQuality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.Model
{
    public class AirQualityShortTerm : IAirQualityShortTerm
    {
        public string Code { get; set; }
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public double? SO2 { get; set; }
        public double? NO2 { get; set; }
        public double? PM10 { get; set; }
        public double? CO { get; set; }
        public double? O3 { get; set; }
        public double? PM25 { get; set; }
        public double? AQI { get; set; }
        public string Type { get; set; }
        public string PrimaryPollutant { get; set; }
        public double? SO2R { get; set; }
        public double? NO2R { get; set; }
        public double? PM10R { get; set; }
        public double? COR { get; set; }
        public double? O3R { get; set; }
        public double? PM25R { get; set; }
        public double? AQIR { get; set; }
    }
}
