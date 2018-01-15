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
using CyberShop1.Models;

namespace CyberShop1.Controllers
{
    public class RegistrationDetailsController : ApiController
    {
        private cshopEntities db = new cshopEntities();

        // GET: api/RegistrationDetails
        public IQueryable<RegistrationDetail> GetRegistrationDetails()
        {
            return db.RegistrationDetails;
        }

        // GET: api/RegistrationDetails/5
        [ResponseType(typeof(RegistrationDetail))]
        public IHttpActionResult GetRegistrationDetail(int id)
        {
            RegistrationDetail registrationDetail = db.RegistrationDetails.Find(id);
            if (registrationDetail == null)
            {
                return NotFound();
            }

            return Ok(registrationDetail);
        }

        // PUT: api/RegistrationDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegistrationDetail(int id, RegistrationDetail registrationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registrationDetail.Sno)
            {
                return BadRequest();
            }

            db.Entry(registrationDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationDetailExists(id))
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

        // POST: api/RegistrationDetails
        [ResponseType(typeof(RegistrationDetail))]
        public IHttpActionResult PostRegistrationDetail(RegistrationDetail registrationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RegistrationDetails.Add(registrationDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (RegistrationDetailExists(registrationDetail.Sno))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = registrationDetail.Sno }, registrationDetail);
        }

        // DELETE: api/RegistrationDetails/5
        [ResponseType(typeof(RegistrationDetail))]
        public IHttpActionResult DeleteRegistrationDetail(int id)
        {
            RegistrationDetail registrationDetail = db.RegistrationDetails.Find(id);
            if (registrationDetail == null)
            {
                return NotFound();
            }

            db.RegistrationDetails.Remove(registrationDetail);
            db.SaveChanges();

            return Ok(registrationDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegistrationDetailExists(int id)
        {
            return db.RegistrationDetails.Count(e => e.Sno == id) > 0;
        }
    }
}