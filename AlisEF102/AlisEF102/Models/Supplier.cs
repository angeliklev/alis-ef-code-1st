using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisEF102.Models
{
    public class Supplier
    {
        public Supplier()
        {
            Assets = new List<Asset>();
        }

        public int SupplierID { get; set; }
        public string SupplierName { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }

    }
}