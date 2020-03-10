﻿using SuncereDataCenter.Basic.Calculators;
using SuncereDataCenter.Core.AirQuality;
using SuncereDataCenter.Core.Extensions;
using SuncereDataCenter.Core.Model;
using SuncereDataCenter.Core.SystemManagement;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SuncereDataCenter.API.Controllers
{
    public class CityAirQualityController : ApiController
    {
        private SuncereDataCenterEntities entities;
        private TokenValidator validator;

        public CityAirQualityController() : base()
        {
            entities = new SuncereDataCenterEntities();
            validator = new TokenValidator(entities);
        }

        public List<AirQualityShortTerm> GetCityHourlyAirQuality(string token, DateTime time, string areaCode)
        {
            if (validator.Validate(token, "CityAirQuality", "GetCityHourlyAirQuality"))
            {
                IQueryable<CityHourlyAirQuality> query = entities.CityHourlyAirQuality.Where(o => o.Time == time);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = entities.Area.FirstOrDefault(o => o.AreaCode == areaCode);
                    if (area == null)
                    {
                        query = query.Where(o => false);
                    }
                    else
                    {
                        string[] cityCodes = area.City.Select(o => o.CityCode).ToArray();
                        query = query.Where(o => cityCodes.Contains(o.Code));
                    }
                }
                List<AirQualityShortTerm> list = query.ToList().Select(o => o.ToAirQualityShortTerm()).ToList();
                AirQualityShortTermCalculator calculator = new AirQualityShortTermCalculator();
                calculator.CalculateRank(list);
                return list;
            }
            else
            {
                return null;
            }
        }

        public List<AirQualityShortTerm> GetCityDailyAirQuality(string token, DateTime time, string areaCode)
        {
            if (validator.Validate(token, "CityAirQuality", "GetCityDailyAirQuality"))
            {
                IQueryable<CityDailyAirQuality> query = entities.CityDailyAirQuality.Where(o => o.Time == time);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = entities.Area.FirstOrDefault(o => o.AreaCode == areaCode);
                    if (area == null)
                    {
                        query = query.Where(o => false);
                    }
                    else
                    {
                        string[] cityCodes = area.City.Select(o => o.CityCode).ToArray();
                        query = query.Where(o => cityCodes.Contains(o.Code));
                    }
                }
                List<AirQualityShortTerm> list = query.ToList().Select(o => o.ToAirQualityShortTerm()).ToList();
                AirQualityShortTermCalculator calculator = new AirQualityShortTermCalculator();
                calculator.CalculateRank(list);
                return list;
            }
            else
            {
                return null;
            }
        }

        public List<AirQualityLongTerm> GetCityMonthlyAirQuality(string token, DateTime time, string areaCode)
        {
            if (validator.Validate(token, "CityAirQuality", "GetCityMonthlyAirQuality"))
            {
                IQueryable<CityMonthlyAirQuality> query = entities.CityMonthlyAirQuality.Where(o => o.Time == time);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = entities.Area.FirstOrDefault(o => o.AreaCode == areaCode);
                    if (area == null)
                    {
                        query = query.Where(o => false);
                    }
                    else
                    {
                        string[] cityCodes = area.City.Select(o => o.CityCode).ToArray();
                        query = query.Where(o => cityCodes.Contains(o.Code));
                    }
                }
                List<AirQualityLongTerm> list = query.ToList().Select(o => o.ToAirQualityLongTerm()).ToList();
                AirQualityLongTermCalculator calculator = new AirQualityLongTermCalculator();
                calculator.CalculateRank(list);
                return list;
            }
            else
            {
                return null;
            }
        }

        public List<AirQualityLongTerm> GetCityQuarterlyAirQuality(string token, DateTime time, string areaCode)
        {
            if (validator.Validate(token, "CityAirQuality", "GetCityQuarterlyAirQuality"))
            {
                IQueryable<CityQuarterlyAirQuality> query = entities.CityQuarterlyAirQuality.Where(o => o.Time == time);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = entities.Area.FirstOrDefault(o => o.AreaCode == areaCode);
                    if (area == null)
                    {
                        query = query.Where(o => false);
                    }
                    else
                    {
                        string[] cityCodes = area.City.Select(o => o.CityCode).ToArray();
                        query = query.Where(o => cityCodes.Contains(o.Code));
                    }
                }
                List<AirQualityLongTerm> list = query.ToList().Select(o => o.ToAirQualityLongTerm()).ToList();
                AirQualityLongTermCalculator calculator = new AirQualityLongTermCalculator();
                calculator.CalculateRank(list);
                return list;
            }
            else
            {
                return null;
            }
        }

        public List<AirQualityLongTerm> GetCityYearlyAirQuality(string token, DateTime time, string areaCode)
        {
            if (validator.Validate(token, "CityAirQuality", "GetCityYearlyAirQuality"))
            {
                IQueryable<CityYearlyAirQuality> query = entities.CityYearlyAirQuality.Where(o => o.Time == time);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = entities.Area.FirstOrDefault(o => o.AreaCode == areaCode);
                    if (area == null)
                    {
                        query = query.Where(o => false);
                    }
                    else
                    {
                        string[] cityCodes = area.City.Select(o => o.CityCode).ToArray();
                        query = query.Where(o => cityCodes.Contains(o.Code));
                    }
                }
                List<AirQualityLongTerm> list = query.ToList().Select(o => o.ToAirQualityLongTerm()).ToList();
                AirQualityLongTermCalculator calculator = new AirQualityLongTermCalculator();
                calculator.CalculateRank(list);
                return list;
            }
            else
            {
                return null;
            }
        }

        public List<AirQualityLongTerm> GetCityAnyTimeRangeAirQuality(string token, DateTime startTime, DateTime endTime, string areaCode)
        {
            if (validator.Validate(token, "CityAirQuality", "GetCityAnyTimeRangeAirQuality"))
            {
                IQueryable<CityDailyAirQuality> query = entities.CityDailyAirQuality.Where(o => o.Time >= startTime && o.Time <= endTime);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = entities.Area.FirstOrDefault(o => o.AreaCode == areaCode);
                    if (area == null)
                    {
                        query = query.Where(o => false);
                    }
                    else
                    {
                        string[] cityCodes = area.City.Select(o => o.CityCode).ToArray();
                        query = query.Where(o => cityCodes.Contains(o.Code));
                    }
                }
                List<AirQualityShortTerm> source = query.ToList().Select(o => o.ToAirQualityShortTerm()).ToList();
                AirQualityLongTermCalculator calculator = new AirQualityLongTermCalculator();
                List<AirQualityLongTerm> list = new List<AirQualityLongTerm>();
                foreach (var cityGroup in source.GroupBy(o => o.Code))
                {
                    AirQualityShortTerm first = cityGroup.First();
                    AirQualityLongTerm item = new AirQualityLongTerm()
                    {
                        Code = first.Code,
                        Time = startTime,
                        Name = first.Name
                    };
                    calculator.Calculate(cityGroup, item);
                    list.Add(item);
                }
                calculator.CalculateRank(list);
                return list;
            }
            else
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entities.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
