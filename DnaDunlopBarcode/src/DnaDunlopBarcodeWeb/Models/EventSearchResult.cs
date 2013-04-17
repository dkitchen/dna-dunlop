using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnaDunlopBarcodeWeb.Models
{
    public class EventSearchResult
    {
        //PartEventHistorySearchResult
        //Event_Log_ID
        //Event_Created
        //Work_Date
        //Work_Shift
        //Event_Name
        //Machine_Name
        //Part_Created
        //Part_Label
        //Green_Tire_Number
        //Goodyear_Serial_Number
        //Operator
        //Data_Name
        //Data_Value

        public long EventLogId { get; set; }
        public DateTime EventCreatedOn { get; set; }
        public DateTime WorkDate { get; set; }
        public string WorkShift { get; set; }
        public string EventName { get; set; }
        public string MachineName { get; set; }
        public DateTime PartCreatedOn { get; set; }
        public string PartLabel { get; set; }
        public string GreenTireNumber { get; set; }
        public string SerialNumber { get; set; }
        public string OperatorNumber { get; set; }
        public string DataName { get; set; }
        public string DataValue { get; set; }


    }
}