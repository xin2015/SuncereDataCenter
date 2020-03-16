namespace SuncereDataCenter.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SuncereDataCenterModel : DbContext
    {
        public SuncereDataCenterModel()
            : base("name=SuncereDataCenter")
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<CityDailyAirQuality> CityDailyAirQuality { get; set; }
        public virtual DbSet<CityHourlyAirQuality> CityHourlyAirQuality { get; set; }
        public virtual DbSet<CityMonthlyAirQuality> CityMonthlyAirQuality { get; set; }
        public virtual DbSet<CityQuarterlyAirQuality> CityQuarterlyAirQuality { get; set; }
        public virtual DbSet<CityYearlyAirQuality> CityYearlyAirQuality { get; set; }
        public virtual DbSet<SuncerePermission> SuncerePermission { get; set; }
        public virtual DbSet<SuncereRole> SuncereRole { get; set; }
        public virtual DbSet<SuncereUser> SuncereUser { get; set; }
        public virtual DbSet<SyncDataQueue> SyncDataQueue { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>()
                .HasMany(e => e.City)
                .WithMany(e => e.Area)
                .Map(m => m.ToTable("AreaCity").MapLeftKey("AreaCode").MapRightKey("CityCode"));

            modelBuilder.Entity<SuncerePermission>()
                .HasMany(e => e.SuncereRole)
                .WithMany(e => e.SuncerePermission)
                .Map(m => m.ToTable("SuncereRolePermission").MapLeftKey("PermissionId").MapRightKey("RoleId"));

            modelBuilder.Entity<SuncereRole>()
                .HasMany(e => e.SuncereUser)
                .WithMany(e => e.SuncereRole)
                .Map(m => m.ToTable("SuncereUserRole").MapLeftKey("RoleId").MapRightKey("UserId"));
        }
    }
}
