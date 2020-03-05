using SuncereDataCenter.Basic.Calculators;
using SuncereDataCenter.Core.AirQuality;
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
