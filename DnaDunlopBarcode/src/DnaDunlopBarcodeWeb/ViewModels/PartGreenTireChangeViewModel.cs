using DnaDunlopBarcodeWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DnaDunlopBarcodeWeb.ViewModels
{
    [MetadataType(typeof(PartGreenTireChangeViewModelMetadata))]
    public class PartGreenTireChangeViewModel
    {
        public string DepartmentName { get; set; }
        public string OperatorSerialNumber { get; set; }
        public string GoodyearSerialNumber { get; set; }
        public string NewGreenTireNumber { get; set; }
        public string OldGreenTireNumber { get; set; }
        public string OldOperator { get; set; }

        //green tire drop list
        public SelectList GreenTireSelectList { get; set; }

        public string Message { get; set; }
    }

    public class PartGreenTireChangeViewModelMetadata
    {
        [Required(ErrorMessage = "Goodyear Serial Number is required.")]
        [Display(Name = "Goodyear Serial Number")]
        [StringLength(10, MinimumLength=10, ErrorMessage = "Goodyear Serial Number must be 10 characters long.")]
        public object GoodyearSerialNumber;

        [Required(ErrorMessage = "Operator Badge Number is required.")]
        [Display(Name = "Operator Number")]
        public object OperatorSerialNumber;

        [Required(ErrorMessage = "New Green Tire is required.")]
        [Display(Name = "New Green Tire Number")]
        public object NewGreenTireNumber;

    }
}