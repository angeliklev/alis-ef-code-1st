using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisEF102.Models
{
    public class Category
    {
        public Category()
        {
            Assets = new List<Asset>();
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", CategoryName);
        }
    }
}