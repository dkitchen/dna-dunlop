using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnaDunlopBarcodeWeb.Models
{
    public class NgTableColumn
    {        
        public string Title { get; set; }
        public string Field { get; set; }
        public bool Visible { get; set; }
        public NgTableColumnFilter Filter { get; set; }

    }

    public class NgTableColumnFilter
    {
        public string Name { get; set; }
    }
}