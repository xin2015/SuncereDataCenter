﻿using AutoMapper;
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
    public class CityYearlyAirQualitySync : SyncBase<CityYearlyAirQuality>
    {
        public CityYearlyAirQualitySync(SuncereDataCenterEntities entities) : base(entities)
        {
            Interval = TimeSpan.FromDays(365);
            StartTimeDeviation = TimeSpan.FromDays(1);
            EndTimeDeviation = TimeSpan.FromDays(2);
        }

        protected override void Sync(SyncDataQueue queue)
        {
            DateTime startTime = new DateTime(queue.Time.Year, 1, 1);
            DateTime endTime = startTime.AddYears(1).AddDays(-1);
            List<CityDailyAirQuality> source = Entities.CityDailyAirQuality.Where(o => o.Time >= startTime && o.Time <= endTime).ToList();
            if (source.Any())
            {
                Mapper.Initialize(x => x.CreateMap<CityDailyAirQuality, AirQualityShortTerm>());
                Mapper.Initialize(x => x.CreateMap<CityMonthlyAirQuality, AirQualityLongTerm>());
                List<AirQualityShortTerm> airQualityShortTermList = Mapper.Map<List<AirQualityShortTerm>>(source);
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
                    list.Add(Mapper.Map<CityMonthlyAirQuality>(item));
                }
                IQueryable<CityMonthlyAirQuality> oldList = Entities.CityMonthlyAirQuality.Where(o => o.Time == startTime);
                Entities.CityMonthlyAirQuality.RemoveRange(oldList);
                Entities.CityMonthlyAirQuality.AddRange(list);
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
