using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AlisFirst.Validation;

namespace AlisFirst.Areas.LMS.ViewModels
{
    [DueDateValidation]
    [LoanDateValidation]
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
        public DateTime DueDate { get; set; }        
    }

    [ReturnLoanValidation]
    public class EditLoanViewModel
    {
        public int LoanID { get; set; }

        public string AssetBarcode { get; set; }

        public string BorrowerBarcode { get; set; }

        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        public string NewCondition { get; set; }

    }

    public class ListOfOnLoans
    {
        public IEnumerable<EditLoanViewModel> OnLoans;
        //Return Date error
        public string SearchKey = "";
    }
}