using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DnaDunlopBarcodeWeb.Models
{
    public class EventSearchResult
    {
        //based on event_log_view_with_shift view

        public decimal EventLogId { get; set; }
        public DateTime EventCreatedOn { get; set; }
        public DateTime WorkDate { get; set; }
        public string WorkShift { get; set; }
        public string EventName { get; set; }
        public decimal? MachineId { get; set; }
        public string MachineName { get; set; }
        public decimal? PartId { get; set; }
        public DateTime? PartCreatedOn { get; set; }
        public string PartLabel { get; set; }
        public string GreenTireNumber { get; set; }
        public string GoodyearSerialNumber { get; set; }
        public string PartHistorylabel { get; set; }
        public decimal? OperatorId { get; set; }
        public string OperatorSerialNumber { get; set; }
        public string OperatorEmployeeNumber { get; set; }
        public string OperatorName { get; set; }
        public decimal? DataId { get; set; }
        public string DataName { get; set; }
        public string DataValue { get; set; }

        public string EventSearchResultId
        {
            get
            {
                return string.Format("{0}-{1}",
                    this.EventLogId,
                    this.DataId);
            }
        }

        public string OperatorNumber
        {
            get
            {
                return string.Format("{0}", this.OperatorName).ToLower() == "unknown"
                    ? this.OperatorSerialNumber
                    : this.OperatorEmployeeNumber;
            }
        }

        public string Operator
        {
            get
            {
                string format = string.Format("{0}{1}", this.OperatorName, this.OperatorNumber).Length > 0
                    ? "{0} ({1})"
                    : "";   //don't bother to diplay space and parens if there's nothing to show for operator

                return string.Format(format,
                    this.OperatorName, this.OperatorNumber);
            }
        }

        public override bool Equals(object obj)
        {
            //return base.Equals(obj);
            var compare = obj as EventSearchResult;
            return compare.EventSearchResultId == this.EventSearchResultId;
        }

        public override int GetHashCode()
        {
            return EventSearchResultId.GetHashCode();
        }

    }
}