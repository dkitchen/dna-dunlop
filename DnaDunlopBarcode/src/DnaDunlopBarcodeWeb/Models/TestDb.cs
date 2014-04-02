using Biggy.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnaDunlopBarcodeWeb.Models
{
    public class TestDb
    {
        public BiggyList<EventSearchResult> EventsSearchResults;
        
        public TestDb()
        {
            EventsSearchResults = new BiggyList<EventSearchResult>(dbPath: HttpRuntime.AppDomainAppPath);
        }
           
    }
}