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
        public UniqueEmailAttribute()
            : base("Email must be unique")
        {
            borrowerRepo = new BorrowerRepository();
        }

        BorrowerRepository borrowerRepo;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (borrowerRepo.CheckEmailUnique((string)value))
                return ValidationResult.Success;
            return new ValidationResult("Email must be unique");
        }
    }
}