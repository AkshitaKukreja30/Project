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
using System.Text;
using System.Web.Http;
using System.Security.Cryptography;
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
            tset = encryptpass(tset);

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

        public string encryptpass(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }

       


        // GET: api/RegistrationDetails/5
        //[HttpGet]

        //[ResponseType(typeof(RegistrationDetail))]

        public IHttpActionResult GetLogin(RegistrationDetail alldetails)
        {
            alldetails.Password = encryptpass(alldetails.Password);
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
            //registrationDetail.Password=encryptpass(registrationDetail.Password);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Check whether this username exists in db or not.

            //true,false
            byte[] salt;
           new RNGCryptoServiceProvider().GetBytes
           (salt= new byte[16]);
           var pbkdf2 = new Rfc2898DeriveBytes(registrationDetail.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            registrationDetail.Password = Convert.ToBase64String(hashBytes);
            //Console.log(registrationDetail.Password);
            
        db.RegistrationDetails.Add(registrationDetail);
            

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
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