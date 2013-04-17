using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnaDunlopBarcodeWeb.ViewModels
{
    //Used by view for entering ANY kind of Department Log data
    public class DepartmentLogViewModel
    {
        public long Id { get; set; }
        public string EventName { get; set; }
        public string MachineName { get; set; }
        public string PartSerialNumber { get; set; }
        public string OperatorSerialNumber { get; set; }
        public string DataName1 { get; set; }
        public string DataValue1 { get; set; }
        public string DataName2 { get; set; }
        public string DataValue2 { get; set; }
        public string DataName3 { get; set; }
        public string DataValue3 { get; set; }
        public string DataName4 { get; set; }
        public string DataValue4 { get; set; }
        public string DataName5 { get; set; }
        public string DataValue5 { get; set; }
    }
}