using DnaDunlopBarcodeWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DnaDunlopBarcodeWeb.Controllers.Rest
{
    public class PartByGoodyearSerialNumberController : ApiController
    {

        private Entities db = new Entities();

        // GET api/Part/5
        public Part GetPart(string goodyearSerialNumber)
        {
            Part part = db.Parts
                .FirstOrDefault(i=>i.GoodyearSerialNumber == goodyearSerialNumber);
            if (part == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return part;
        }
    }
}
