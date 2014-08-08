using DnaDunlopBarcodeWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DnaDunlopBarcodeWeb.Controllers.Rest
{
    public class EventSearchController : ApiController
    {
        private Entities _db = new Entities();


        // GET api/DepartmentLog
        public IEnumerable<EventSearchResult> Get()
        {

            //gets the last 1000 events
            var yesterday = DateTime.Now.AddDays(-1);
            return GetEvents()
                .Where(i => i.EventCreatedOn > yesterday)
                .OrderByDescending(i => i.EventLogId)
                .Take(1000)
                .AsEnumerable();
        }

        // GET api/EventSearch/899016405767518
        public IEnumerable<EventSearchResult> Get(string barcode)
        {
            return GetEvents()
                .Where(i => i.PartLabel == barcode)
                .OrderBy(i => i.EventLogId)
                .OrderBy(i => i.DataId);

        }


        // GET api/EventSearch/?greenTireNumber=89901&beginDate=2014-02-17&endDate=2014-02-18
        public IEnumerable<EventSearchResult> Get(string greenTireNumber, DateTime beginDate, DateTime endDate)
        {
            //1. get all the barcodes that match the green tire number and had their Runout Complete event between the begin and end dates
            //2. get all the events (or maybe just the data needed for qaData) for all the barcodes

            var barcodes = from l in _db.EVENT_LOG
                           join p in _db.Parts on l.PART_ID equals p.Id
                           join e in _db.EVENTs on l.EVENT_ID equals e.EVENT_ID
                           where p.GreenTireNumber == greenTireNumber
                           && e.NAME == "RunoutComplete"
                           && l.CREATED > beginDate
                           && l.CREATED < endDate
                           select p.SerialNumber;

            var DistinctBarcodes = barcodes.Distinct()
                .ToArray();

            var FifteenDigitBarcodes = DistinctBarcodes
                .Where(i => i.Trim().Length == 15).ToArray();

            var LessThanFifteenDigitBarcodes = DistinctBarcodes
                .Where(i => i.Trim().Length < 15).ToArray();

            var eventNames = new List<string>() { 
                "GoodTire",
                "CureTireLog",
                "RunoutComplete",
                "TireInPress",
                "ForceComplete",
                "RankComplete",
                "BalanceComplete"
            };

            var dummyBarcodeEventNames = new List<string>()
            {
                "RunoutComplete",
                "ForceComplete",
                "RankComplete",
                "BalanceComplete"
            };

            //TEST
            //var goodTireEvents = GetEvents()
            //    .Where(i => i.EventName == "GoodTire"
            //        && i.EventCreatedOn > beginDate
            //        && i.EventCreatedOn < endDate)
            //    .ToList();

            var events = GetEvents()
                .Where(i => FifteenDigitBarcodes.Contains(i.PartLabel)
                && eventNames.Contains(i.EventName))
                .ToList();

            //TEST
            //goodTireEvents = events
            //    .Where(i => i.EventName == "GoodTire")
            //    .ToList();

            var dummyBarcodeEvents = GetEvents()
                .Where(i => LessThanFifteenDigitBarcodes.Contains(i.PartLabel)
                && dummyBarcodeEventNames.Contains(i.EventName)
                && i.EventCreatedOn > beginDate
                && i.EventCreatedOn < endDate)
                .ToList();

            var combined = events
                .Concat(dummyBarcodeEvents);

            return combined;

        }

        private IQueryable<EventSearchResult> GetEvents()
        {
            return from l in _db.EVENT_LOG
                   join ls in _db.EVENT_LOG_SHIFT on l.EVENT_LOG_ID equals ls.EVENT_LOG_ID    //inner join
                   from e in _db.EVENTs.Where(e => l.EVENT_ID == e.EVENT_ID).DefaultIfEmpty() //outer join
                   from m in _db.MACHINEs.Where(m => l.MACHINE_ID == m.MACHINE_ID).DefaultIfEmpty()
                   from o in _db.OPERATORs.Where(o => l.OPERATOR_ID == o.OPERATOR_ID).DefaultIfEmpty()
                   from p in _db.Parts.Where(p => l.PART_ID == p.Id).DefaultIfEmpty()
                   from ph in _db.PART_HISTORY.Where(ph => l.PART_ID == ph.PART_ID).DefaultIfEmpty()
                   from d in _db.EVENT_LOG_DATA.Where(d => l.EVENT_LOG_ID == d.EVENT_LOG_ID).DefaultIfEmpty()
                   from ed in _db.EVENT_DATA.Where(ed => d.EVENT_DATA_ID == ed.EVENT_DATA_ID).DefaultIfEmpty()
                   select new EventSearchResult()
                   {
                       EventLogId = l.EVENT_LOG_ID,
                       EventCreatedOn = l.CREATED,
                       EventName = e.NAME,
                       MachineId = m.MACHINE_ID,
                       MachineName = m.NAME,
                       PartId = p.Id,
                       PartCreatedOn = p.CreatedOn,
                       PartLabel = p.SerialNumber,
                       GreenTireNumber = p.GreenTireNumber,
                       GoodyearSerialNumber = p.GoodyearSerialNumber,
                       PartHistorylabel = ph.SERIAL_NUMBER,
                       OperatorId = o.OPERATOR_ID,
                       OperatorEmployeeNumber = o.EMPLOYEE_NUMBER,
                       OperatorSerialNumber = o.SERIAL_NUMBER,
                       OperatorName = o.NAME,
                       DataId = ed.EVENT_DATA_ID,
                       DataName = ed.NAME,
                       DataValue = d.DATA_VALUE,
                       WorkDate = ls.WORK_DATE,
                       WorkShift = ls.WORK_SHIFT
                   };
        }
    }
}
