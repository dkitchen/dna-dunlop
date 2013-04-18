using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace DnaDunlopBarcodeWeb.ViewModels
{
    public static class Extensions
    {
        //http://www.peterprovost.org/blog/2012/06/16/unit-testing-asp-dot-net-web-api/
        /// <summary>
        /// HTTP Post needs special setup when calling ApiController directly (without http request)
        /// </summary>
        /// <param name="api"></param>
        public static void InitializePost(this ApiController controller)
        {
            var config = new HttpConfiguration();            
            var request = new HttpRequestMessage(HttpMethod.Post, "http://MOCKhost/api/MOCKController");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "MOCK" } });
            
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
        }

        //http://www.extensionmethod.net/csharp/string/defaultifempty
        /// <summary>
        /// Helps when viewModels have null string properties
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <param name="considerWhiteSpaceIsEmpty"></param>
        /// <returns></returns>
        public static string DefaultIfEmpty(this string str, string defaultValue, bool considerWhiteSpaceIsEmpty = false)
        {
            return (considerWhiteSpaceIsEmpty ? string.IsNullOrWhiteSpace(str) : string.IsNullOrEmpty(str)) ? defaultValue : str;
        }
    }
}