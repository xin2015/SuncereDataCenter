namespace SuncereDataCenter.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EnvPublishStdModel : DbContext
    {
        public EnvPublishStdModel()
            : base("name=EnvPublishStd")
        {
        }

        public virtual DbSet<AQIDataPublishHistory> AQIDataPublishHistory { get; set; }
        public virtual DbSet<AQIDataPublishLive> AQIDataPublishLive { get; set; }
        public virtual DbSet<CityAQIPublishHistory> CityAQIPublishHistory { get; set; }
        public virtual DbSet<CityAQIPublishLive> CityAQIPublishLive { get; set; }
        public virtual DbSet<CityDayAQIPublishHistory> CityDayAQIPublishHistory { get; set; }
        public virtual DbSet<CityDayAQIPublishLive> CityDayAQIPublishLive { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.PositionName)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.StationCode)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.CO)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.CO_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.Latitude)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.Longitude)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.Measure)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.NO2)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.NO2_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.O3)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.O3_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.O3_8h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.O3_8h_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.PM10)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.PM10_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.PM2_5)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.PM2_5_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.SO2)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.SO2_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.AQI)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.PrimaryPollutant)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.Quality)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishHistory>()
                .Property(e => e.Unheathful)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.PositionName)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.StationCode)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.CO)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.CO_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.Latitude)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.Longitude)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.Measure)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.NO2)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.NO2_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.O3)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.O3_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.O3_8h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.O3_8h_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.PM10)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.PM10_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.PM2_5)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.PM2_5_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.SO2)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.SO2_24h)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.AQI)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.PrimaryPollutant)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.Quality)
                .IsUnicode(false);

            modelBuilder.Entity<AQIDataPublishLive>()
                .Property(e => e.Unheathful)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishHistory>()
                .Property(e => e.CO)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishHistory>()
                .Property(e => e.NO2)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishHistory>()
                .Property(e => e.O3)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishHistory>()
                .Property(e => e.PM10)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishHistory>()
                .Property(e => e.PM2_5)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishHistory>()
                .Property(e => e.SO2)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishHistory>()
                .Property(e => e.AQI)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishHistory>()
                .Property(e => e.PrimaryPollutant)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishHistory>()
                .Property(e => e.Quality)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishHistory>()
                .Property(e => e.Measure)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishHistory>()
                .Property(e => e.Unheathful)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishLive>()
                .Property(e => e.CO)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishLive>()
                .Property(e => e.NO2)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishLive>()
                .Property(e => e.O3)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishLive>()
                .Property(e => e.PM10)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishLive>()
                .Property(e => e.PM2_5)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishLive>()
                .Property(e => e.SO2)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishLive>()
                .Property(e => e.AQI)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishLive>()
                .Property(e => e.PrimaryPollutant)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishLive>()
                .Property(e => e.Quality)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishLive>()
                .Property(e => e.Measure)
                .IsUnicode(false);

            modelBuilder.Entity<CityAQIPublishLive>()
                .Property(e => e.Unheathful)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishHistory>()
                .Property(e => e.SO2_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishHistory>()
                .Property(e => e.CO_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishHistory>()
                .Property(e => e.NO2_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishHistory>()
                .Property(e => e.O3_8h_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishHistory>()
                .Property(e => e.PM10_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishHistory>()
                .Property(e => e.PM2_5_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishHistory>()
                .Property(e => e.AQI)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishHistory>()
                .Property(e => e.PrimaryPollutant)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishHistory>()
                .Property(e => e.Quality)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishHistory>()
                .Property(e => e.Measure)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishHistory>()
                .Property(e => e.Unheathful)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishLive>()
                .Property(e => e.SO2_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishLive>()
                .Property(e => e.CO_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishLive>()
                .Property(e => e.NO2_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishLive>()
                .Property(e => e.O3_8h_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishLive>()
                .Property(e => e.PM10_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishLive>()
                .Property(e => e.PM2_5_24h)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishLive>()
                .Property(e => e.AQI)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishLive>()
                .Property(e => e.PrimaryPollutant)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishLive>()
                .Property(e => e.Quality)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishLive>()
                .Property(e => e.Measure)
                .IsUnicode(false);

            modelBuilder.Entity<CityDayAQIPublishLive>()
                .Property(e => e.Unheathful)
                .IsUnicode(false);
        }
    }
}
