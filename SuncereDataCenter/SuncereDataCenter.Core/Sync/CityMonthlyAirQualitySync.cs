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
    public class CityMonthlyAirQualitySync : SyncBase<CityMonthlyAirQuality>
    {
        public CityMonthlyAirQualitySync(SuncereDataCenterModel model) : base(model)
        {

        }

        protected override void Sync(SyncDataQueue queue)
        {
            DateTime startTime = new DateTime(queue.Time.Year, queue.Time.Month, 1);
            DateTime endTime = startTime.AddMonths(1).AddDays(-1);
            List<CityDailyAirQuality> source = Model.CityDailyAirQuality.Where(o => o.Time >= startTime && o.Time <= endTime).ToList();
            if (source.Any())
            {
                List<AirQualityShortTerm> airQualityShortTermList = source.Select(o => o.ToAirQualityShortTerm()).ToList();
                List<CityMonthlyAirQuality> list = new List<CityMonthlyAirQuality>();
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
                    list.Add(item.ToCityMonthlyAirQuality());
                }
                IQueryable<CityMonthlyAirQuality> oldList = Model.CityMonthlyAirQuality.Where(o => o.Time == startTime);
                Model.CityMonthlyAirQuality.RemoveRange(oldList);
                Model.CityMonthlyAirQuality.AddRange(list);
                queue.Status = true;
            }
            queue.LastTime = DateTime.Now;
        }

        protected override DateTime GetNextTime(DateTime time)
        {
            return time.AddMonths(1);
        }
    }
}
