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
    
    public partial class EVENT_LOG
    {
        public decimal EVENT_LOG_ID { get; set; }
        public System.DateTime CREATED { get; set; }
        public decimal EVENT_ID { get; set; }
        public Nullable<decimal> MACHINE_ID { get; set; }
        public Nullable<decimal> PART_ID { get; set; }
        public Nullable<decimal> OPERATOR_ID { get; set; }
    
        public virtual EVENT EVENT { get; set; }
        public virtual MACHINE MACHINE { get; set; }
        public virtual PART PART { get; set; }
        public virtual OPERATOR OPERATOR { get; set; }
    }
}
