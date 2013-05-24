using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AlisFirst.Models
{
    public class Loan
    {
        public int LoanID { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int AssetID { get; set; }
        public int BorrowerID { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Borrower Borrower { get; set; }
    }
}