using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AlisFirst.Validation;

namespace AlisFirst.ViewModels
{
    public class CreateLoanViewModel
    {
        [AssetBarcodeValidation]
        public string AssetBarcode { get; set;} 
        [BorrowerBarcodeValidation]
        public string BorrowerBarcode { get; set; }

        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }        
    }

    //[LoanDateValidation]
    public class EditLoanViewModel
    {
        public int LoanID { get; set; }

        public string AssetBarcode { get; set; }

        public string BorrowerBarcode { get; set; }

        [DataType(DataType.Date)]
        //[LoanDateValidation]
        public DateTime LoanDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        //public string? NewCondition { get; set; }

        //public int AssetConditionID { get; set; }
        //public string Description { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        //public DateTime? IssuedDate { get; set; }
        //public int AssetID { get; set; }
    }
}