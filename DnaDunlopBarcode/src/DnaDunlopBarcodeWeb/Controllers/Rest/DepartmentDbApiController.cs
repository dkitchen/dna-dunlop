using DnaDunlopBarcodeWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DnaDunlopBarcodeWeb.Controllers.Rest
{
    /// <summary>
    /// This ApiController enables connecting to different databases depending on which department was selected.
    /// Other Api Controllers can inherit this ability
    /// </summary>
    public class DepartmentDbApiController : ApiController
    {
        private Entities _db;
        private string _department;

        public DepartmentDbApiController(string department)
        {
            _department = department;
            _db = new Entities();
            string connString = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>()
                .FirstOrDefault(i => i.Name.ToUpper().StartsWith(department.ToUpper()))
                .ConnectionString;

            _db.Database.Connection.ConnectionString
                = connString;

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