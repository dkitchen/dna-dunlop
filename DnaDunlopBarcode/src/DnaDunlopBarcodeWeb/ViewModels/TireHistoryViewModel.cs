using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnaDunlopBarcodeWeb.ViewModels
{
    public class TireHistoryViewModel
    {
        public string TireLabel { get; set; }
        public int ResultCountPerPage { get; set; }
        public string Message { get; set; }

        public List<EventLogSearchResult> SearchResults { get; set; }
    }

    public class EventLogSearchResult
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public string Shift { get; set; }
        public string EventName { get; set; }
        public string MachineName { get; set; }
        public DateTime TireCreateDate { get; set; }
        public string TireSerialNumber { get; set; }
        public string GreenTireNumber { get; set; }
        public string GoodyearSerialNumber { get; set; }
        public string Operator { get; set; }
        public string DataName { get; set; }
        public string DataValue { get; set; }


    }
}