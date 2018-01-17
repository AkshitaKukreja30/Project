using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Description;
using System.Web.Http.Results;
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
        [HttpGet]

        [ResponseType(typeof(RegistrationDetail))]
        public IHttpActionResult GetRegistrationDetail()
        {
            String username = Request.RequestUri.Query;
            String actualusername=username.Substring(10);
            RegistrationDetail ifuserexists = db.RegistrationDetails.Where(a => a.User_Name.Equals(actualusername)).FirstOrDefault();
            if (ifuserexists == null)
            {
                return NotFound();
            }


            return Ok(ifuserexists);
        }


        // GET: api/RegistrationDetails/6
        [ResponseType(typeof(RegistrationDetail))]
        public IHttpActionResult GetRegistrationDetail2()
        {
            String detailsforlogin = Request.RequestUri.Query;
            var separator1 = detailsforlogin.IndexOf('=');
            separator1++;
            
            var s = detailsforlogin.IndexOf('&');
            String actualusernameforlogin = detailsforlogin.Substring(separator1, s-separator1);
            var l = detailsforlogin.Length;           
            var separator2 = detailsforlogin.LastIndexOf('=');
            separator2++;
            String tset = detailsforlogin.Substring(separator2);
            
            RegistrationDetail ifuserexists = db.RegistrationDetails.Where(a => a.User_Name.Equals(actualusernameforlogin)).FirstOrDefault();
            if (ifuserexists == null)
            {
                return NotFound();
            }

            else if (ifuserexists != null && tset.Equals(ifuserexists.Password))
            {
                return Ok();

            }

            else
                return NotFound();
                
                     

            
        }












        // GET: api/RegistrationDetails/5
        //[HttpGet]

        //[ResponseType(typeof(RegistrationDetail))]

        public IHttpActionResult GetLogin(RegistrationDetail alldetails)
        {
            var checkfordetails = db.RegistrationDetails;
            RegistrationDetail ifuserexists = checkfordetails.Where(a => a.User_Name.Equals(alldetails.User_Name)).FirstOrDefault();
            //User foundUser = db.User.Where(a => a.UserName.Equals(user.UserName)).FirstOrDefault();
            if (ifuserexists == null)
            {
                return NotFound();
            }
            else if (ifuserexists != null && ifuserexists.Password.Equals(alldetails.Password))
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Accepted, "Login Successful"));
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Ambiguous, "Incorrect Password"));
            }
               

        }

        //public IHttpActionResult GetRegistrationDetail(int id)
        //{
        //    RegistrationDetail registrationDetail = db.RegistrationDetails.Find(id);
        //    if (registrationDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(registrationDetail);
        //}

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
            //Check whether this username exists in db or not.

            //true,false


            
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

    internal class JsonResult
    {
        public RegistrationDetail Data { get; set; }
        public object JsonRequestBehavior { get; set; }
    }
}