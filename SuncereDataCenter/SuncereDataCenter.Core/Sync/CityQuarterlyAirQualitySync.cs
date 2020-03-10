using AutoMapper;
using SuncereDataCenter.Core.AirQuality;
using SuncereDataCenter.Core.Model;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.Sync
{
    public class CityQuarterlyAirQualitySync : SyncBase<CityQuarterlyAirQuality>
    {
        public CityQuarterlyAirQualitySync(SuncereDataCenterEntities entities) : base(entities)
        {
            Interval = TimeSpan.FromDays(90);
            StartTimeDeviation = TimeSpan.FromDays(1);
            EndTimeDeviation = TimeSpan.FromDays(2);
        }

        protected override void Sync(SyncDataQueue queue)
        {
            DateTime startTime = new DateTime(queue.Time.Year, (queue.Time.Month - 1) / 3 * 3 + 1, 1);
            DateTime endTime = startTime.AddMonths(3).AddDays(-1);
            List<CityDailyAirQuality> source = Entities.CityDailyAirQuality.Where(o => o.Time >= startTime && o.Time <= endTime).ToList();
            if (source.Any())
            {
                List<AirQualityShortTerm> airQualityShortTermList = Mapper.Map<List<AirQualityShortTerm>>(source);
                List<CityQuarterlyAirQuality> list = new List<CityQuarterlyAirQuality>();
                AirQualityLongTermCalculator calculator = new AirQualityLongTermCalculator();
                foreach (var cityGroup in airQualityShortTermList.GroupBy(o => o.Code))
                {
                    AirQualityShortTerm first = cityGroup.First();
                    AirQualityLongTerm item = new AirQualityLongTerm()
                    {
                        Code = first.Code,
                        Time = startTime,
                        Name = first.Name
                    };
                    calculator.Calculate(cityGroup, item);
                    list.Add(Mapper.Map<CityQuarterlyAirQuality>(item));
                }
                IQueryable<CityQuarterlyAirQuality> oldList = Entities.CityQuarterlyAirQuality.Where(o => o.Time == startTime);
                Entities.CityQuarterlyAirQuality.RemoveRange(oldList);
                Entities.CityQuarterlyAirQuality.AddRange(list);
                queue.Status = true;
            }
            queue.LastTime = DateTime.Now;
        }

        protected override DateTime GetTime()
        {
            return DateTime.Today.AddDays(-1);
        }
    }
}
