using SuncereDataCenter.Basic.Extensions;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.Sync
{
    public class CityHourlyAirQualitySync : SyncBase<CityHourlyAirQuality>
    {
        public CityHourlyAirQualitySync(SuncereDataCenterModel model) : base(model)
        {
            Interval = TimeSpan.FromHours(1);
            StartTimeDeviation = TimeSpan.FromMinutes(28);
            EndTimeDeviation = TimeSpan.FromDays(5);
        }

        protected override void Sync(SyncDataQueue queue)
        {
            using (EnvPublishStdModel std = new EnvPublishStdModel())
            {
                List<CityAQIPublishHistory> source = std.CityAQIPublishHistory.Where(o => o.TimePoint == queue.Time).ToList();
                if (source.Any())
                {
                    List<CityHourlyAirQuality> list = new List<CityHourlyAirQuality>();
                    foreach (CityAQIPublishHistory sourceItem in source)
                    {
                        CityHourlyAirQuality item = new CityHourlyAirQuality()
                        {
                            Code = sourceItem.CityCode.ToString(),
                            Time = queue.Time,
                            Name = sourceItem.Area,
                            SO2 = sourceItem.SO2.ToNullableDouble(),
                            NO2 = sourceItem.NO2.ToNullableDouble(),
                            PM10 = sourceItem.PM10.ToNullableDouble(),
                            CO = sourceItem.CO.ToNullableDouble(),
                            O3 = sourceItem.O3.ToNullableDouble(),
                            PM25 = sourceItem.PM2_5.ToNullableDouble(),
                            AQI = sourceItem.AQI.ToNullableDouble(),
                            Type = sourceItem.Quality,
                            PrimaryPollutant = sourceItem.PrimaryPollutant
                        };
                        list.Add(item);
                    }
                    Model.CityHourlyAirQuality.AddRange(list);
                    queue.Status = true;
                }
                queue.LastTime = DateTime.Now;
            }
        }

        protected override DateTime GetTime()
        {
            return DateTime.Today.AddHours(DateTime.Now.Hour);
        }
    }
}
