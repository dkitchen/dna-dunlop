using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnaDunlopBarcodeWeb.Models
{
    public class QaData
    {
        public string GreenTireCode { get; set; }
        public string Barcode { get; set; }
        public DateTime? FirstStageBuildDate { get; set; }

        public string FirstStageTBM { get; set; }
        public string FirstStageBuilder { get; set; }
        public DateTime? SecondStageBuildDate { get; set; }
        public string SecondStageBuildTBM { get; set; }
        public string SecondStageBuilder { get; set; }
        public string CureOperator { get; set; }
        public DateTime? CureDate { get; set; }
        public string Pot { get; set; }
        public string MoldNum { get; set; }
        public string TuoId { get; set; }
        public DateTime? TestedDate { get; set; }
        public string TuoTireCode { get; set; }
        public string CWLoad { get; set; }
        public string CwInflation { get; set; }
        public string CwRpp { get; set; }
        public string CwRh1 { get; set; }
        public string CwLpp { get; set; }
        public string CwCrro { get; set; }
        public string CcwLpp { get; set; }
        public string CcwConicity { get; set; }
        public string GrLoad { get; set; }
        public string GrInflation { get; set; }
        public string GrRpp { get; set; }
        public string GrRh1 { get; set; }
        public string GrLpp { get; set; }
        public string GrConicity { get; set; }
        public string TuoGrade { get; set; }
        public string UpperBalance { get; set; }
        public string UpperBalanceAngle { get; set; }
        public string LowerBalance { get; set; }
        public string LowerBalanceAngle { get; set; }
        public string StaticBalance { get; set; }
        public string StaticBalanceAngle { get; set; }
        public string DynamicBalance { get; set; }
        public string DynamicAngle { get; set; }
        public string BalanceGrade { get; set; }
    }
}