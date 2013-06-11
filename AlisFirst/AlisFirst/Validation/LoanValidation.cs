using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AlisFirst.DAL;
using AlisFirst.Models;
using AlisFirst.Areas.LMS.ViewModels;


namespace AlisFirst.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class BorrowerBarcodeValidation : ValidationAttribute
    {
        LoanRepository LoanRepo = new LoanRepository();
        public BorrowerBarcodeValidation()
            : base("Unknown Error")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Please input Asset Barcode");

            string BorrowerBarcode = (string)value;

            if(LoanRepo.getBorrowerID(BorrowerBarcode.Trim()) == -1)
                return new ValidationResult("Not Found Borrower");

            return ValidationResult.Success;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AssetBarcodeValidation : ValidationAttribute
    {
        LoanRepository LoanRepo = new LoanRepository();
        public AssetBarcodeValidation()
            : base("Unknown Error")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Please input Asset Barcode");

            string AssetBarcode = (string)value;

            if (LoanRepo.getAssetID(AssetBarcode.Trim()) == -1)
                return new ValidationResult("Not Found Asset");
            else
            {
                if (LoanRepo.IsLoanable(LoanRepo.getAssetID(AssetBarcode.Trim())))
                    return new ValidationResult("This asset is not loanable");

                if (LoanRepo.IsOnLoan(LoanRepo.getAssetID(AssetBarcode.Trim())))
                {
                    return new ValidationResult("Already On Loan");
                }
            }
            return ValidationResult.Success;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class LoanDateValidation : ValidationAttribute
    {

        LoanRepository LoanRepo = new LoanRepository();
        public LoanDateValidation()
            : base("Incorrect Format")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            CreateLoanViewModel clvm = (CreateLoanViewModel)value;

            DateTime bed = LoanRepo.getBorrowerExpiryDate(clvm.BorrowerBarcode);
            if(bed.CompareTo(clvm.LoanDate) < 0)
                return new ValidationResult("Borrower Expiry Date must be before Loan Date");

            return ValidationResult.Success;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DueDateValidation: ValidationAttribute
    {
        LoanRepository LoanRepo = new LoanRepository();

        public DueDateValidation()
            : base("Incorrect Format")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            CreateLoanViewModel clvm = (CreateLoanViewModel)value;


            if (clvm.DueDate != null)
            {
                DateTime dueDate = (DateTime)clvm.DueDate;
                DateTime loanDate = (DateTime)clvm.LoanDate;
                if (dueDate.CompareTo(loanDate) < 0)
                    return new ValidationResult("Due Date must be before Loan Date");
           
            }

            return ValidationResult.Success;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ReturnLoanValidation : ValidationAttribute
    {
        LoanRepository LoanRepo = new LoanRepository();
        public ReturnLoanValidation()
            :base("Unknow Error")
        {}

         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            EditLoanViewModel elvm = (EditLoanViewModel)value;

            if (elvm.ReturnDate.CompareTo(elvm.LoanDate) < 0)
                return new ValidationResult("Return Date cannot be before Loan Date");

             //If an asset is returned late, we still need to put it into system, so I leave empty for now

             //if(elvm.DueDate != null)
             //{
             //    if(elvm.ReturnDate.CompareTo(elvm.DueDate) >0)
             //       return new ValidationResult(""
             //}
            return ValidationResult.Success;
        }
    }

    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class SearchValidation : ValidationAttribute
    //{
    //    LoanRepository LoanRepo = new LoanRepository();
    //    public SearchValidation()
    //        :base("Unknow Error")
    //    {}
    //     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        if (value == null)
    //            return new ValidationResult("Please input Asset Barcode");

    //        string AssetBarcode = (string)value;

    //        if (LoanRepo.getAssetID(AssetBarcode.Trim()) == -1)
    //            return new ValidationResult("Not Found Asset");
    //        else
    //        {
    //            if (LoanRepo.IsLoanable(LoanRepo.getAssetID(AssetBarcode.Trim())))
    //                return new ValidationResult("This asset is not loanable");

    //            if (LoanRepo.IsOnLoan(LoanRepo.getAssetID(AssetBarcode.Trim())))
    //            {
    //                return new ValidationResult("Already On Loan");
    //            }
    //        }
    //        return ValidationResult.Success;
    //    }
    //}

}