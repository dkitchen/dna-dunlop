using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DnaDunlopBarcodeWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute(
                name: "DepartmentApi",
                routeTemplate: "{department}/api/{controller}/{id}",
                defaults: new { department = "Test", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
