using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnaDunlopBarcodeWeb.Models
{
    public class EventSearch
    {

        public string ConnectionStringName { get; set; }
        public int RowLimit { get; set; }
        public int CurrentPage { get; set; }

        
    }

    public class PartEventSearch : EventSearch
    {
        public string PartSerialNumber { get; set; }
    }
}