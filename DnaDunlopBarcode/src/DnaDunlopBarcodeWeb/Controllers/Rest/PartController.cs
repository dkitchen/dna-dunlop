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
    public class PartController : ApiController
    {
        private Entities db = new Entities();

        // GET api/Part
        public IEnumerable<Part> GetParts()
        {
            return db.Parts.AsEnumerable();
        }

        // GET api/Part/5
        public Part GetPart(decimal id)
        {
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return part;
        }

        // PUT api/Part/5
        public HttpResponseMessage PutPart(decimal id, Part part)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != part.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(part).State = EntityState.Modified;

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

        // POST api/Part
        public HttpResponseMessage PostPart(Part part)
        {
            if (ModelState.IsValid)
            {
                db.Parts.Add(part);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, part);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = part.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Part/5
        public HttpResponseMessage DeletePart(decimal id)
        {
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Parts.Remove(part);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, part);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}