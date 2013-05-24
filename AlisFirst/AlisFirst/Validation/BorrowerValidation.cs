using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AlisFirst.DAL;
using AlisFirst.Models;

namespace AlisFirst.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public class UniqueEmailAttribute : ValidationAttribute
    {
        BorrowerRepository borrowerRepo;
        public UniqueEmailAttribute()
            : base("Unknown Error")
        {
            borrowerRepo = new BorrowerRepository();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty((string)value))
                return ValidationResult.Success;
            if (!borrowerRepo.CheckEmailUnique((string)value))
                return new ValidationResult("Email must be unique");

            return ValidationResult.Success;
            
        }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueBarcodeAttribute : ValidationAttribute
    {
        BorrowerRepository borrowerRepo;
        public UniqueBarcodeAttribute()
            : base("Unknown Error")
        {
            borrowerRepo = new BorrowerRepository();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty((string)value))
                return new ValidationResult("Bar code is required");
            if (!borrowerRepo.CheckBarcodeUnique((string)value))
                return new ValidationResult("Bar code must be unique");

            return ValidationResult.Success;
        }
    }
}