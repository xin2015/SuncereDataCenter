using SuncereDataCenter.Basic.Calculators;
using SuncereDataCenter.Core.AirQuality;
using SuncereDataCenter.Core.System;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SuncereDataCenter.API.Controllers
{
    public class CityAirQualityController : ApiController
    {
        private SuncereDataCenterEntities db = new SuncereDataCenterEntities();

        public List<CityHourlyAirQuality> GetCityHourlyAirQuality(string token, DateTime time, string areaCode)
        {
            if (TokenValidator.Validate(token, "CityAirQuality", "GetCityHourlyAirQuality"))
            {
                IQueryable<CityHourlyAirQuality> query = db.CityHourlyAirQuality.Where(o => o.Time == time);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = db.Area.FirstOrDefault(o => o.AreaCode == areaCode);
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
                AirQualityShortTermCalculator calculator = new AirQualityShortTermCalculator();
                calculator.CalculatorRank(query);
                return query.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<CityDailyAirQuality> GetCityDailyAirQuality(string token, DateTime time, string areaCode)
        {
            if (TokenValidator.Validate(token, "CityAirQuality", "GetCityDailyAirQuality"))
            {
                IQueryable<CityDailyAirQuality> query = db.CityDailyAirQuality.Where(o => o.Time == time);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = db.Area.FirstOrDefault(o => o.AreaCode == areaCode);
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
                AirQualityShortTermCalculator calculator = new AirQualityShortTermCalculator();
                calculator.CalculatorRank(query);
                return query.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<CityMonthlyAirQuality> GetCityMonthlyAirQuality(string token, DateTime time, string areaCode)
        {
            if (TokenValidator.Validate(token, "CityAirQuality", "GetCityMonthlyAirQuality"))
            {
                IQueryable<CityMonthlyAirQuality> query = db.CityMonthlyAirQuality.Where(o => o.Time == time);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = db.Area.FirstOrDefault(o => o.AreaCode == areaCode);
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
                AirQualityLongTermCalculator calculator = new AirQualityLongTermCalculator();
                calculator.CalculatorRank(query);
                return query.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<CityQuarterlyAirQuality> GetCityQuarterlyAirQuality(string token, DateTime time, string areaCode)
        {
            if (TokenValidator.Validate(token, "CityAirQuality", "GetCityQuarterlyAirQuality"))
            {
                IQueryable<CityQuarterlyAirQuality> query = db.CityQuarterlyAirQuality.Where(o => o.Time == time);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = db.Area.FirstOrDefault(o => o.AreaCode == areaCode);
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
                AirQualityLongTermCalculator calculator = new AirQualityLongTermCalculator();
                calculator.CalculatorRank(query);
                return query.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<CityYearlyAirQuality> GetCityYearlyAirQuality(string token, DateTime time, string areaCode)
        {
            if (TokenValidator.Validate(token, "CityAirQuality", "GetCityYearlyAirQuality"))
            {
                IQueryable<CityYearlyAirQuality> query = db.CityYearlyAirQuality.Where(o => o.Time == time);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = db.Area.FirstOrDefault(o => o.AreaCode == areaCode);
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
                AirQualityLongTermCalculator calculator = new AirQualityLongTermCalculator();
                calculator.CalculatorRank(query);
                return query.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<CityYearlyAirQuality> GetCityAnyTimeRangeAirQuality(string token, DateTime startTime, DateTime endTime, string areaCode)
        {
            if (TokenValidator.Validate(token, "CityAirQuality", "GetCityAnyTimeRangeAirQuality"))
            {
                IQueryable<CityDailyAirQuality> query = db.CityDailyAirQuality.Where(o => o.Time >= startTime && o.Time <= endTime);
                if (!string.IsNullOrEmpty(areaCode))
                {
                    Area area = db.Area.FirstOrDefault(o => o.AreaCode == areaCode);
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
                List<CityDailyAirQuality> source = query.ToList();
                AirQualityLongTermCalculator calculator = new AirQualityLongTermCalculator();
                List<CityYearlyAirQuality> list = new List<CityYearlyAirQuality>();
                foreach (var cityGroup in source.GroupBy(o => o.Code))
                {
                    CityYearlyAirQuality item = new CityYearlyAirQuality();
                    calculator.Calculate(cityGroup, item);
                    list.Add(item);
                }
                calculator.CalculatorRank(list);
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
