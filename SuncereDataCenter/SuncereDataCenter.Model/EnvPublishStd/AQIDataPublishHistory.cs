namespace SuncereDataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AQIDataPublishHistory")]
    public partial class AQIDataPublishHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 0)]
        public DateTime TimePoint { get; set; }

        [StringLength(50)]
        public string Area { get; set; }

        [StringLength(40)]
        public string PositionName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string StationCode { get; set; }

        [StringLength(8)]
        public string CO { get; set; }

        [StringLength(8)]
        public string CO_24h { get; set; }

        [StringLength(15)]
        public string Latitude { get; set; }

        [StringLength(15)]
        public string Longitude { get; set; }

        [StringLength(255)]
        public string Measure { get; set; }

        [StringLength(5)]
        public string NO2 { get; set; }

        [StringLength(5)]
        public string NO2_24h { get; set; }

        [StringLength(5)]
        public string O3 { get; set; }

        [StringLength(5)]
        public string O3_24h { get; set; }

        [StringLength(5)]
        public string O3_8h { get; set; }

        [StringLength(5)]
        public string O3_8h_24h { get; set; }

        [StringLength(5)]
        public string PM10 { get; set; }

        [StringLength(5)]
        public string PM10_24h { get; set; }

        [StringLength(5)]
        public string PM2_5 { get; set; }

        [StringLength(5)]
        public string PM2_5_24h { get; set; }

        [StringLength(5)]
        public string SO2 { get; set; }

        [StringLength(5)]
        public string SO2_24h { get; set; }

        [StringLength(5)]
        public string AQI { get; set; }

        [StringLength(255)]
        public string PrimaryPollutant { get; set; }

        [StringLength(255)]
        public string Quality { get; set; }

        [StringLength(255)]
        public string Unheathful { get; set; }
    }
}
