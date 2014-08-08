using DnaDunlopBarcodeWeb.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DnaDunlopBarcodeWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        //TODO get rid of this when we hook up to Live DB
        //public static TestDb TestDb { get; set; }

        protected void Application_Start()
        {
            //MvcApplication.TestDb = new TestDb();

            //Environment.SetEnvironmentVariable("PATH", @"C:\ORACLE\Oracle11XCopy32bit;C:\ORACLE\Oracle11XCopy32bit\ODP.NET4\BIN;C:\ORACLE\Oracle11XCopy32bit\odp.net4\odp.net\bin\4;", EnvironmentVariableTarget.Process);
            //Environment.SetEnvironmentVariable("ORACLE_HOME", @"C:\ORACLE\Oracle11XCopy32bit;", EnvironmentVariableTarget.Process);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);



        }
    }
}