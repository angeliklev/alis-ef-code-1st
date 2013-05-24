using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AlisFirst.DAL;
using AlisFirst.Models;
using AlisFirst.ViewModels;


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
    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class NewConditionValidation : ValidationAttribute
    //{
    //    LoanRepository LoanRepo = new LoanRepository();
    //    public NewConditionValidation()
    //        : base("Unknown Error")
    //    { }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        string newCondition = (string)value;
    //        if (newCondition != null)
    //            LoanRepo.newCondition(newCondition);

    //        return ValidationResult.Success;
    //    }
    //}

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class LoanDateValidation : ValidationAttribute
    {

        LoanRepository LoanRepo = new LoanRepository();
        public LoanDateValidation()
            : base("Incorrect Format")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext, string test)
        {
            //EditLoanViewModel elvm = (EditLoanViewModel)value;
            //elvm.BorrowerBarcode;
            if (value == null)
                return new ValidationResult("Please input loan date");

            

            return ValidationResult.Success;
        }
    }

    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    //public class DueDateValidation : ValidationAttribute
    //{
    //    LoanRepository LoanRepo = new LoanRepository();

    //    public DueDateValidation()
    //        : base("Incorrect Format")
    //    { }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        if (value != null)
    //        {
    //            DateTime duteDate = (DateTime)value;

    //        }
    //        return ValidationResult.Success;
    //    }
    //}

}