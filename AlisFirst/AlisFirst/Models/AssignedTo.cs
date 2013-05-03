using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisFirst.Models
{
    public class AssignedTo
    {
        public int AssignedToID { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int BorrowerID { get; set; }
        public int AssetID { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Borrower Borrower { get; set; }
    }
}