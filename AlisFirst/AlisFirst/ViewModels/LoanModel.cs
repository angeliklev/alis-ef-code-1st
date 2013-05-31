using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AlisFirst.Validation;
using AlisFirst.Models;

namespace AlisFirst.ViewModels
{
    [DueDateValidation]
    [LoanDateValidation]
    public class CreateLoanViewModel
    {
        // === Included by Jon ===
        // {

            public Asset Asset { get; set; }

            public Borrower Borrower { get; set; }

            public IEnumerable<CheckListItem> CheckListItems { get; set; }
            
        // }
        // =======================

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
}