using Common.Logging;
using SuncereDataCenter.Basic.Extensions;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.Sync
{
    public class CityDailyAirQualitySync : SyncBase<CityDailyAirQuality>
    {
        public CityDailyAirQualitySync(SuncereDataCenterModel model) : base(model)
        {
            Interval = TimeSpan.FromDays(1);
            StartTimeDeviation = TimeSpan.FromDays(1);
            EndTimeDeviation = TimeSpan.FromDays(6);
        }

        protected override void Sync(SyncDataQueue queue)
        {
            using (EnvPublishStdModel std = new EnvPublishStdModel())
            {
                List<CityDayAQIPublishHistory> source = std.CityDayAQIPublishHistory.Where(o => o.TimePoint == queue.Time).ToList();
                if (source.Any())
                {
                    List<CityDailyAirQuality> list = new List<CityDailyAirQuality>();
                    foreach (CityDayAQIPublishHistory sourceItem in source)
                    {
                        CityDailyAirQuality item = new CityDailyAirQuality()
                        {
                            Code = sourceItem.CityCode.ToString(),
                            Time = queue.Time,
                            Name = sourceItem.Area,
                            SO2 = sourceItem.SO2_24h.ToNullableDouble(),
                            NO2 = sourceItem.NO2_24h.ToNullableDouble(),
                            PM10 = sourceItem.PM10_24h.ToNullableDouble(),
                            CO = sourceItem.CO_24h.ToNullableDouble(),
                            O3 = sourceItem.O3_8h_24h.ToNullableDouble(),
                            PM25 = sourceItem.PM2_5_24h.ToNullableDouble(),
                            AQI = sourceItem.AQI.ToNullableDouble(),
                            Type = sourceItem.Quality,
                            PrimaryPollutant = sourceItem.PrimaryPollutant
                        };
                        list.Add(item);
                    }
                    Model.CityDailyAirQuality.AddRange(list);
                    queue.Status = true;
                }
                queue.LastTime = DateTime.Now;
            }
        }

        protected override DateTime GetTime()
        {
            return DateTime.Today.AddDays(-1);
        }
    }
}
