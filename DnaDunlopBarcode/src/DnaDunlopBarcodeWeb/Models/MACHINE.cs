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
    
    public partial class MACHINE
    {
        public MACHINE()
        {
            this.EVENT_LOG = new HashSet<EVENT_LOG>();
        }
    
        public decimal MACHINE_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<decimal> DEPARTMENT_ID { get; set; }
        public string FUNCTIONAL_LOCATION { get; set; }
    
        public virtual ICollection<EVENT_LOG> EVENT_LOG { get; set; }
    }
}
