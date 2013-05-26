using DnaDunlopBarcodeWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DnaDunlopBarcodeWeb.Controllers.Rest
{
    public class DepartmentDbApiController : ApiController
    {
        private Entities _db;
        private string _department;

        public DepartmentDbApiController(string department)
        {
            _department = department;
            _db = new Entities();
            _db.Database.Connection.ConnectionString
                = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>()
                .FirstOrDefault(i => i.Name.ToUpper().StartsWith(department.ToUpper()))
                .ConnectionString;
        }
        
        protected Entities Db
        {
            get
            {
                return _db;
            }
        }
    }
}