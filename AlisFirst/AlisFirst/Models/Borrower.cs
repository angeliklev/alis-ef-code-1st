using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisFirst.Models
{
    public class Borrower
    {
        public int BorrowerID { get; set; }
        public string Surname { get; set; }
        public string BarCode { get; set; }
        public string GivenName { get; set; }
        
        public DateTime? BorrowerExpiryDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsEmployee { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<AssignedTo> AssignedTos { get; set; }
    }
}