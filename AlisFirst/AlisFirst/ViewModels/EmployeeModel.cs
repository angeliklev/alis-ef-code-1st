using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AlisFirst.ViewModels
{
    public class ListEmployeeViewModel
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
}