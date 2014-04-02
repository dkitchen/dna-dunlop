using DnaDunlopBarcodeWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DnaDunlopBarcodeWeb.Controllers.Rest
{
    public class QaColumnsController : ApiController
    {
        public IEnumerable<NgTableColumn> Get()
        {

            var cols = new List<NgTableColumn>();
            cols.Add(new NgTableColumn() { Title = "Green Tire Code", Field = "GreenTireCode", Visible = true, Filter = new NgTableColumnFilter() { Name = "text" } });
            cols.Add(new NgTableColumn() { Title = "Bar Code", Field = "BarCode", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Date/Time 1st Stage Build", Field = "FirstStageBuildDate", Visible = true });
            cols.Add(new NgTableColumn() { Title = "First Stage TBM", Field = "FirstStageTBM", Visible = true });
            cols.Add(new NgTableColumn() { Title = "First Stage Builder", Field = "FirstStageBuilder", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Date/Time 2nd Stage Build", Field = "SecondStageBuildDate", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Second Stage TBM", Field = "SecondStageBuildTBM", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Second Stage Builder", Field = "SecondStageBuilder", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Cure Operator", Field = "CureOperator", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Date/Time Cure", Field = "CureDate", Visible = true });
            cols.Add(new NgTableColumn() { Title = "POT", Field = "Pot", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Mold Num", Field = "MoldNum", Visible = true });
            cols.Add(new NgTableColumn() { Title = "TUO ID", Field = "TuoId", Visible = true });
            cols.Add(new NgTableColumn() { Title = "TUO Tire Code", Field = "TuoTireCode", Visible = true });
            cols.Add(new NgTableColumn() { Title = "CW Load", Field = "CWLoad", Visible = true });
            cols.Add(new NgTableColumn() { Title = "CW Inflation", Field = "CwInflation", Visible = true });
            cols.Add(new NgTableColumn() { Title = "CW RPP", Field = "CwRpp", Visible = true });
            cols.Add(new NgTableColumn() { Title = "CW RH1", Field = "CwRh1", Visible = true });
            cols.Add(new NgTableColumn() { Title = "CW LPP", Field = "CwLpp", Visible = true });
            cols.Add(new NgTableColumn() { Title = "CW CRRO", Field = "CwCrro", Visible = true });
            cols.Add(new NgTableColumn() { Title = "CCW LPP", Field = "CcwLpp", Visible = true });
            cols.Add(new NgTableColumn() { Title = "CCW Conicity", Field = "CcwConicity", Visible = true });
            cols.Add(new NgTableColumn() { Title = "GR Load", Field = "GrLoad", Visible = true });
            cols.Add(new NgTableColumn() { Title = "GR Inflation", Field = "GrInflation", Visible = true });
            cols.Add(new NgTableColumn() { Title = "GR RPP", Field = "GrRpp", Visible = true });
            cols.Add(new NgTableColumn() { Title = "GR RH1", Field = "GrRh1", Visible = true });
            cols.Add(new NgTableColumn() { Title = "GR LPP", Field = "GrLpp", Visible = true });
            cols.Add(new NgTableColumn() { Title = "GR Conicity", Field = "GrConicity", Visible = true });
            cols.Add(new NgTableColumn() { Title = "TUO Grade", Field = "TuoGrade", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Upper Balance", Field = "UpperBalance", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Upper Balance Angle", Field = "UpperBalanceAngle", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Lower Balance", Field = "LowerBalance", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Lower Balance Angle", Field = "LowerBalanceAngle", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Static Balance", Field = "StaticBalance", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Static Balance Angle", Field = "StaticBalanceAngle", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Dynamic Balance", Field = "DynamicBalance", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Dynamic Angle", Field = "DynamicAngle", Visible = true });
            cols.Add(new NgTableColumn() { Title = "Balance Grade", Field = "BalanceGrade", Visible = true });

            return cols;

        }
    }
}
