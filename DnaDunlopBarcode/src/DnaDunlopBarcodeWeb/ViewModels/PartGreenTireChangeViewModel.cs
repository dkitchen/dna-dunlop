using DnaDunlopBarcodeWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DnaDunlopBarcodeWeb.ViewModels
{
    public class PartGreenTireChangeViewModel
    {
        public DepartmentLog DepartmentLog { get; set; }
        public string GoodyearSerialNumber { get; set; }
        public string NewGreenTireNumber { get; set; }
        public string OldGreenTireNumber { get; set; }

        //green tire drop list
        public SelectList GreenTireSelectList { get; set; }

        public string Message { get; set; }
    }
}