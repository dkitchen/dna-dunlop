using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DnaDunlopBarcodeWeb.ViewModels
{
    [MetadataType(typeof(PartSerialNumberChangeViewModelMetadata))]
    public class PartSerialNumberChangeViewModel
    {
        
            public string OperatorSerialNumber { get; set; }
            public string OldGoodyearSerialNumber { get; set; }
            public string NewGoodyearSerialNumber { get; set; }

            public string Message { get; set; }
        
    }

    public class PartSerialNumberChangeViewModelMetadata
    {
        [Required(ErrorMessage = "Old Goodyear Serial Number is required.")]
        [Display(Name = "Old Goodyear Serial Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Old Serial Number must be 10-15 characters long.")]
        public object OldGoodyearSerialNumber;

        [Required(ErrorMessage = "New Goodyear Serial Number is required.")]
        [Display(Name = "New Goodyear Serial Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "New Serial Number must be 10 characters long.")]
        public object NewGoodyearSerialNumber;

        [Required(ErrorMessage = "Operator Badge Number is required.")]
        [Display(Name = "Operator Badge Number")]
        public object OperatorSerialNumber;

    }
}