using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SuncereDataCenter.Model;

namespace SuncereDataCenter.API.Controllers
{
    public class MonthlyAirQualitiesController : ApiController
    {
        private SuncereDataCenterEntities db = new SuncereDataCenterEntities();

        // GET: api/MonthlyAirQualities
        public IQueryable<CityMonthlyAirQuality> GetMonthlyAirQuality()
        {
            return db.CityMonthlyAirQuality;
        }

        // GET: api/MonthlyAirQualities/5
        [ResponseType(typeof(CityMonthlyAirQuality))]
        public IHttpActionResult GetMonthlyAirQuality(string id)
        {
            CityMonthlyAirQuality monthlyAirQuality = db.CityMonthlyAirQuality.Find(id);
            if (monthlyAirQuality == null)
            {
                return NotFound();
            }

            return Ok(monthlyAirQuality);
        }

        // PUT: api/MonthlyAirQualities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMonthlyAirQuality(string id, CityMonthlyAirQuality monthlyAirQuality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != monthlyAirQuality.Code)
            {
                return BadRequest();
            }

            db.Entry(monthlyAirQuality).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonthlyAirQualityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MonthlyAirQualities
        [ResponseType(typeof(CityMonthlyAirQuality))]
        public IHttpActionResult PostMonthlyAirQuality(CityMonthlyAirQuality monthlyAirQuality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CityMonthlyAirQuality.Add(monthlyAirQuality);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MonthlyAirQualityExists(monthlyAirQuality.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = monthlyAirQuality.Code }, monthlyAirQuality);
        }

        // DELETE: api/MonthlyAirQualities/5
        [ResponseType(typeof(CityMonthlyAirQuality))]
        public IHttpActionResult DeleteMonthlyAirQuality(string id)
        {
            CityMonthlyAirQuality monthlyAirQuality = db.CityMonthlyAirQuality.Find(id);
            if (monthlyAirQuality == null)
            {
                return NotFound();
            }

            db.CityMonthlyAirQuality.Remove(monthlyAirQuality);
            db.SaveChanges();

            return Ok(monthlyAirQuality);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MonthlyAirQualityExists(string id)
        {
            return db.CityMonthlyAirQuality.Count(e => e.Code == id) > 0;
        }
    }
}