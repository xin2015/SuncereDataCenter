//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SuncereDataCenter.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CityHourlyAirQuality
    {
        public string Code { get; set; }
        public System.DateTime Time { get; set; }
        public string Name { get; set; }
        public Nullable<double> SO2 { get; set; }
        public Nullable<double> NO2 { get; set; }
        public Nullable<double> PM10 { get; set; }
        public Nullable<double> CO { get; set; }
        public Nullable<double> O3 { get; set; }
        public Nullable<double> PM25 { get; set; }
        public Nullable<double> AQI { get; set; }
        public string Type { get; set; }
        public string PrimaryPollutant { get; set; }
    }
}
