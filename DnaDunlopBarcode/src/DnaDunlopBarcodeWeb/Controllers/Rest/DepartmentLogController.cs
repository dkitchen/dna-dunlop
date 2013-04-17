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
    public class DepartmentLogController : ApiController
    {
        private Entities db = new Entities();

        // GET api/DepartmentLog
        public IEnumerable<DEPARTMENT_LOG> Get()
        {
            //only get the last 100 items
            return db.DEPARTMENT_LOG
                .OrderByDescending(i=>i.DEPARTMENT_LOG_ID)
                .Take(100)
                .AsEnumerable();
        }

        // GET api/DepartmentLog/5
        public DEPARTMENT_LOG Get(decimal id)
        {
            DEPARTMENT_LOG department_log = db.DEPARTMENT_LOG.Find(id);
            if (department_log == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return department_log;
        }

        // PUT api/DepartmentLog/5
        public HttpResponseMessage Put(decimal id, DEPARTMENT_LOG department_log)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != department_log.DEPARTMENT_LOG_ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(department_log).State = EntityState.Modified;

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
        public HttpResponseMessage Post(DEPARTMENT_LOG department_log)
        {
            if (ModelState.IsValid)
            {
                db.DEPARTMENT_LOG.Add(department_log);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, department_log);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = department_log.DEPARTMENT_LOG_ID }));
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
            DEPARTMENT_LOG department_log = db.DEPARTMENT_LOG.Find(id);
            if (department_log == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.DEPARTMENT_LOG.Remove(department_log);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, department_log);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}