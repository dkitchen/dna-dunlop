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
using System.Text;

namespace DnaDunlopBarcodeWeb.Controllers.Rest
{
    public class GreenTireAndOperatorController : ApiController
    {
        private Entities db = new Entities();

        //// GET api/PartPastGreenTireAndOperator
        //public IEnumerable<Part> GetParts()
        //{
        //    return db.Parts.AsEnumerable();
        //}

        // GET api/GreenTireAndOperator/5
        public GTN_AND_OPERATOR Get(string id)
        {
            //id = goodyearSerialNumber
            var gtnAndOp = db.GTN_AND_OPERATOR.Where(i => i.GOODYEAR_SERIAL_NUMBER == id)
                .FirstOrDefault();

            if (gtnAndOp == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return gtnAndOp;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}