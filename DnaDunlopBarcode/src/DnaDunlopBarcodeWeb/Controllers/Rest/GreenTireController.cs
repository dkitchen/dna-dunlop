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
    public class GreenTireController : ApiController
    {
        private Entities db = new Entities();

        // GET api/GreenTire
        public IEnumerable<GreenTire> GetGreenTires()
        {
            return db.GreenTires
                .OrderBy(i=>i.GreenTireNumber)
                .AsEnumerable();
        }

        // GET api/GreenTire/5
        public GreenTire GetGreenTire(decimal id)
        {
            GreenTire greentire = db.GreenTires.Find(id);
            if (greentire == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return greentire;
        }

        // PUT api/GreenTire/5
        public HttpResponseMessage PutGreenTire(decimal id, GreenTire greentire)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != greentire.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(greentire).State = EntityState.Modified;

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

        // POST api/GreenTire
        public HttpResponseMessage PostGreenTire(GreenTire greentire)
        {
            if (ModelState.IsValid)
            {
                db.GreenTires.Add(greentire);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, greentire);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = greentire.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/GreenTire/5
        public HttpResponseMessage DeleteGreenTire(decimal id)
        {
            GreenTire greentire = db.GreenTires.Find(id);
            if (greentire == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.GreenTires.Remove(greentire);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, greentire);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}