using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AlisFirst.Validation;
using AlisFirst.Models;
namespace AlisFirst.Areas.LMS.ViewModels
{
    public class CreateBorrowerViewModel
    {
        public string Surname { get; set; }
        [UniqueBarcodeAttribute]
        [Required(ErrorMessage = "Bar code is required")][Display(Name="Bar Code")]
        public string BarCode { get; set; }
        [Display(Name = "Given Name")]
        public string GivenName { get; set; }
        [DataType(DataType.Date)][Display(Name = "Borrower Expiry Date")][DateValidation]
        public DateTime? BorrowerExpiryDate { get; set; }
        [UniqueEmailAttribute]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Employee")]
        public bool IsEmployee { get; set; }
    }

    public class EditBorrowerViewModel
    {
        [Required]
        public int BorrowerID { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Bar Code")]
        public string BarCode { get; set; }
        [Display(Name = "Given Name")]
        public string GivenName { get; set; }
        [DataType(DataType.Date)][Display(Name = "Borrower Expiry Date")][DisplayFormat(DataFormatString="{0:dd/mm/yy}")][DateValidation]
        public DateTime? BorrowerExpiryDate { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Employee")]
        public bool IsEmployee { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
    }

    public class DeleteBorrowerViewModel
    {
        [Required]
        public int BorrowerID { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Bar Code")]
        public string BarCode { get; set; }
        [Display(Name = "Given Name")]
        public string GivenName { get; set; }
        [DataType(DataType.Date)][Display(Name = "Borrower Expiry Date")]
        public DateTime? BorrowerExpiryDate { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Employee")]
        public bool IsEmployee { get; set; }
    }

    public class ListBorrowerViewModel
    {
        public IEnumerable<EditBorrowerViewModel> ListOfBorrowers;
        public String SearchFor;
    }
}