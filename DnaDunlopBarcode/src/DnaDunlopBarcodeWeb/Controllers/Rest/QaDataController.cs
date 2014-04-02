using DnaDunlopBarcodeWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DnaDunlopBarcodeWeb.Controllers.Rest
{
    public class QaDataController : ApiController
    {
        // GET /api/QaData/?greenTireNumber=89901&beginDate=2014-02-17&endDate=2014-02-18
        public IEnumerable<QaData> Get(string greenTireNumber, DateTime? beginDate, DateTime? endDate)
        {
            if (greenTireNumber == null || greenTireNumber.Length < 5 || !beginDate.HasValue || !endDate.HasValue)
            {
                //nothing to see here until you give us valid search criteria
                return new List<QaData>();
            }

            var events = new EventSearchController().Get(greenTireNumber, beginDate.Value, endDate.Value);
            
            //distinct list of barcodes
            var barcodes = events
                .OrderByDescending(i=>i.EventCreatedOn)
                .Select(i => i.PartLabel)
                .Distinct();

            var qaItems = new List<QaData>();

            foreach (var barcode in barcodes)
            {
                //data related to this barcode
                var qaItem = new QaData();

                var eventItems = events
                    .Where(i => i.PartLabel == barcode);

                var buildEvents = GetEvents(eventItems, "GoodTire");

                //hopefully there are at least 2 of these
                EventSearchResult firstBuildEvent = buildEvents.FirstOrDefault();
                if (firstBuildEvent != null)
                {
                    qaItem.GreenTireCode = firstBuildEvent.GreenTireNumber;
                    qaItem.Barcode = firstBuildEvent.PartLabel;
                    qaItem.FirstStageBuildDate = firstBuildEvent.EventCreatedOn;
                    qaItem.FirstStageTBM = firstBuildEvent.MachineName;
                    qaItem.FirstStageBuilder = firstBuildEvent.Operator;
                }

                EventSearchResult secondBuildEvent = null;
                if (buildEvents.Count() > 1)
                {
                    secondBuildEvent = buildEvents.LastOrDefault();
                    qaItem.SecondStageBuildDate = secondBuildEvent.EventCreatedOn;
                    qaItem.SecondStageBuildTBM = secondBuildEvent.MachineName;
                    qaItem.SecondStageBuilder = secondBuildEvent.Operator;
                }

                var cureEvents = GetEvents(eventItems, "CureTireLog");
                var firstCureEvent = cureEvents.FirstOrDefault();
                if (firstCureEvent != null)
                {
                    qaItem.CureOperator = firstCureEvent.Operator;
                    qaItem.CureDate = firstCureEvent.EventCreatedOn;
                }
                qaItem.Pot = GetDataValue(cureEvents, "pot");
                qaItem.MoldNum = GetDataValue(cureEvents, "MoldNum");

                var runoutEvents = GetEvents(eventItems, "RunoutComplete");
                var firstRounoutEvent = runoutEvents.FirstOrDefault();
                if(firstRounoutEvent !=null)
                {

                    qaItem.TuoId = firstRounoutEvent.MachineName;
                    qaItem.TestedDate = firstRounoutEvent.EventCreatedOn;
                }
                qaItem.GrRpp = GetDataValue(runoutEvents, "Grind Center Radial Peak to Peak (in)");
                qaItem.TuoTireCode = GetDataValue(runoutEvents, "Machine Code Found");
                
                qaItem.GrRh1 = GetDataValue(runoutEvents, "Grind Center Radial 1st Harmonic");
                qaItem.CwCrro = GetDataValue(runoutEvents, "CW Center Radial Peak to Peak (in)");

                var forceEvents = GetEvents(eventItems, "forcecomplete");
                qaItem.CWLoad = GetDataValue(forceEvents, "Load (Lbs)");
                qaItem.CwInflation = GetDataValue(forceEvents, "Inflation (PSI)");
                qaItem.CwLpp = GetDataValue(forceEvents, "CW Lateral FV Overall");
                qaItem.CwRpp = GetDataValue(forceEvents, "CW Radial FV Overall");
                qaItem.CcwLpp = GetDataValue(forceEvents, "CCW Lateral FV Overall");
                qaItem.GrLpp = GetDataValue(forceEvents, "Grind Lateral FV Overall");
                qaItem.CwRh1 = GetDataValue(forceEvents, "CW Radial FV 1st Harmonic");

                var rankEvents = GetEvents(eventItems, "RankComplete");
                qaItem.GrConicity = GetDataValue(rankEvents, "Grind Conicity");
                qaItem.TuoGrade = GetDataValue(rankEvents, "Mark");
                qaItem.CcwConicity = GetDataValue(rankEvents, "CCW Conicity");
                
                var balanceEvents = GetEvents(eventItems, "BalanceComplete");
                qaItem.UpperBalance = GetDataValue(balanceEvents, "Upper (g)");
                qaItem.UpperBalanceAngle = GetDataValue(balanceEvents, "Upper Angle (deg)");
                qaItem.LowerBalance = GetDataValue(balanceEvents, "Lower (g)");
                qaItem.LowerBalanceAngle = GetDataValue(balanceEvents, "Lower Angle (deg)");
                qaItem.StaticBalance = GetDataValue(balanceEvents, "Static (g cm)");
                qaItem.StaticBalanceAngle = GetDataValue(balanceEvents, "Static Angle (deg)");
                qaItem.DynamicBalance = GetDataValue(balanceEvents, "Couple (g cm cm)");
                qaItem.DynamicAngle = GetDataValue(balanceEvents, "Couple Angle (deg)");
                qaItem.BalanceGrade = GetDataValue(balanceEvents, "Balance Grade");

                //add qa item to collection
                qaItems.Add(qaItem);

            }

            return qaItems;

        }

        private IEnumerable<EventSearchResult> GetEvents(IEnumerable<EventSearchResult> items, string eventName)
        {
            var itemsByEventName = items
                .Where(i => string.Format("{0}", i.EventName).Trim().ToLower() == string.Format("{0}", eventName).Trim().ToLower())
                .OrderBy(i => i.EventSearchResultId);
            return itemsByEventName;
        }

        private string GetDataValue(IEnumerable<EventSearchResult> items, string dataName)
        {
            var item = items
                    .Where(i => string.Format("{0}", i.DataName).Trim().ToLower() == string.Format("{0}", dataName).Trim().ToLower())
                    .OrderBy(i => i.EventSearchResultId)
                    .FirstOrDefault();

            if (item != null)
            {
                return item.DataValue;
            }

            return null;
        }



    }


}
