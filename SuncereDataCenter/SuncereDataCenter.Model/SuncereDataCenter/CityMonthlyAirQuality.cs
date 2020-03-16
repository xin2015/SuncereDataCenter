namespace SuncereDataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CityMonthlyAirQuality")]
    public partial class CityMonthlyAirQuality
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string Code { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Time { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public double? SO2 { get; set; }

        public double? NO2 { get; set; }

        public double? PM10 { get; set; }

        public double? CO { get; set; }

        public double? O3 { get; set; }

        public double? PM25 { get; set; }

        public double? AQCI { get; set; }

        public double? StandardDays { get; set; }

        [StringLength(255)]
        public string PrimaryPollutant { get; set; }
    }
}
