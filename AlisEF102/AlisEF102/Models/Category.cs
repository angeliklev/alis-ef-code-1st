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
            AssetModels = new List<AssetModel>();
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<AssetModel> AssetModels { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", CategoryName);
        }
    }
}