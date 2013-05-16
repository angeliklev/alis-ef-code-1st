using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AlisFirst.Models
{
    public class Borrower
    {
        [ScaffoldColumn(false)]
        public int BorrowerID { get; set; }
        
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        
        [Required]
        [Display(Name = "Barcode")]
        public string BarCode { get; set; }
        
        [Required]
        [Display(Name = "Given Name")]
        public string GivenName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        public DateTime BorrowerExpiryDate { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Employee")]
        public bool IsEmployee { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<AssignedTo> AssignedTos { get; set; }
    }
}