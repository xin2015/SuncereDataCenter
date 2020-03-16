using SuncereDataCenter.Core.AirQuality;
using SuncereDataCenter.Core.Extensions;
using SuncereDataCenter.Core.Model;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuncereDataCenter.Core.Sync
{
    public class CityYearlyAirQualitySync : SyncBase<CityYearlyAirQuality>
    {
        public CityYearlyAirQualitySync(SuncereDataCenterModel model) : base(model)
        {
            Interval = TimeSpan.FromDays(365);
            StartTimeDeviation = TimeSpan.FromDays(1);
            EndTimeDeviation = TimeSpan.FromDays(2);
        }

        protected override void Sync(SyncDataQueue queue)
        {
            DateTime startTime = new DateTime(queue.Time.Year, 1, 1);
            DateTime endTime = startTime.AddYears(1).AddDays(-1);
            List<CityDailyAirQuality> source = Model.CityDailyAirQuality.Where(o => o.Time >= startTime && o.Time <= endTime).ToList();
            if (source.Any())
            {
                List<AirQualityShortTerm> airQualityShortTermList = source.Select(o => o.ToAirQualityShortTerm()).ToList();
                List<CityYearlyAirQuality> list = new List<CityYearlyAirQuality>();
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
                    list.Add(item.ToCityYearlyAirQuality());
                }
                IQueryable<CityYearlyAirQuality> oldList = Model.CityYearlyAirQuality.Where(o => o.Time == startTime);
                Model.CityYearlyAirQuality.RemoveRange(oldList);
                Model.CityYearlyAirQuality.AddRange(list);
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
