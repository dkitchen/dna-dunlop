using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnaDunlopBarcodeWeb.Models;


namespace DnaDunlopBarcodeWeb.ViewModels
{
    //Used by view for entering ANY kind of Department Log data
    public class DepartmentLogViewModel
    {
        public string DepartmentName { get; set; }
        public DepartmentLog SelectedDepartmentLog { get; set; }
        public IEnumerable<DepartmentLog> DepartmentLogs { get; set; }
        public string Message { get; set; }

    }
}