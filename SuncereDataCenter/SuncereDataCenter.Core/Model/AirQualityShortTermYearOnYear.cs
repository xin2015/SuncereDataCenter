using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.Model
{
    public class AirQualityShortTermYearOnYear : AirQualityShortTerm
    {
        public double? AQIYOY { get; set; }
        public double? AQIYOYR { get; set; }
    }
}
