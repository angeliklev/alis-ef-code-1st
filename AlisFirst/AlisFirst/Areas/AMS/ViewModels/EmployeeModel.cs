using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AlisFirst.Models;
using AlisFirst.Validation;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class ListEmployeeViewModel
    {
        public IEnumerable<EditEmployeeViewModel> ListOfEmployees;
        public string SearchKey = "";
    }

    public class CreateEmployeeViewModel
    {
        [Display(Name = "Bar Code")]
        [UniqueBarcode]
        public string BarCode { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Given Name")]
        public string GivenName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Borrower Expiry Date")]
        public DateTime? BorrowerExpiryDate { get; set; }
        [UniqueEmail]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class EditEmployeeViewModel
    {
        [Required]
        [Key]
        public int BorrowerID { get; set; }
        [Display(Name = "Bar Code")]
        public string BarCode { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Given Name")]
        public string GivenName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Borrower Expiry Date")][DisplayFormat(DataFormatString="{0:dd/mm/yy}")]
        public DateTime? BorrowerExpiryDate { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public bool IsEmployee { get; set; }
    }

    public class DeleteEmployeeViewModel
    {
        [Required][Key]
        public int BorrowerID { get; set; }
        [Display(Name = "Bar Code")]
        public string BarCode { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Given Name")]
        public string GivenName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Borrower Expiry Date")]
        public DateTime? BorrowerExpiryDate { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class AssignToEmployeeViewModel
    {
        public string EmployeeBarCode;
        public DateTime AssignDate;
        public bool ConditionChecked;
        public bool ChecklistChecked;
        public IEnumerable<AssignedTo> History;
    }
}