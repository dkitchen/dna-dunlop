//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DnaDunlopBarcodeWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PART_HISTORY
    {
        public decimal PART_HISTORY_ID { get; set; }
        public decimal PART_ID { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string GOODYEAR_SERIAL_NUMBER { get; set; }
        public string GREEN_TIRE_NUMBER { get; set; }
        public System.DateTime CREATED_ON { get; set; }
    
        public virtual Part PART { get; set; }
    }
}
