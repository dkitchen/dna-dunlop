using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DnaDunlopBarcodeWeb.Models;

namespace DnaDunlopBarcodeWeb.Controllers.Rest
{
    public class EventLogSearchController : ApiController
    {
        private Entities db = new Entities();

        // GET api/DepartmentLog
        public IEnumerable<DepartmentLog> Get()
        {
            //only get the last 100 items
            return db.DepartmentLogs
                .OrderByDescending(i => i.Id)
                .Take(100)
                .AsEnumerable();
        }

        // GET api/DepartmentLog/5
        public DepartmentLog Get(decimal id)
        {
            var log = db.DepartmentLogs.Find(id);
            if (log == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return log;
        }

        // PUT api/DepartmentLog/5
        public HttpResponseMessage Put(decimal id, DepartmentLog log)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != log.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(log).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/DepartmentLog
        public HttpResponseMessage Post(DepartmentLog log)
        {
            if (ModelState.IsValid)
            {
                //set date if not set already
                if (log.CreatedOn < DateTime.Now.AddDays(-1))
                {
                    log.CreatedOn = DateTime.Now;
                }
                db.DepartmentLogs.Add(log);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, log);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = log.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/DepartmentLog/5
        public HttpResponseMessage Delete(decimal id)
        {
            var log = db.DepartmentLogs.Find(id);
            if (log == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.DepartmentLogs.Remove(log);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, log);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}