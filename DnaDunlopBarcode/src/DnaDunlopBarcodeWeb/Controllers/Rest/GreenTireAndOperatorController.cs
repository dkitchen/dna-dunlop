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
using System.Configuration;

namespace DnaDunlopBarcodeWeb.Controllers.Rest
{
    public class GreenTireAndOperatorController : ApiController
    {

        Entities _db;
   
        // GET api/GreenTireAndOperator/5
        public GTN_AND_OPERATOR Get(string department, string id)
        {
            //id = goodyearSerialNumber
            _db = new Entities();

            //TODO - handle this in some new base class method...
            _db.Database.Connection.ConnectionString
                = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>()
                .FirstOrDefault(i => i.Name.ToUpper().StartsWith(department.ToUpper()))
                .ConnectionString;

            var gtnAndOp = _db.GTN_AND_OPERATOR.Where(i => i.GOODYEAR_SERIAL_NUMBER == id)
                .FirstOrDefault();

            if (gtnAndOp == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return gtnAndOp;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}