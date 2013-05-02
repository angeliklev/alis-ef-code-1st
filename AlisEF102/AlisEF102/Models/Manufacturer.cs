using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisEF102.Models
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            AssetModels = new List<AssetModel>();
        }

        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }

        public virtual ICollection<AssetModel> AssetModels { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", ManufacturerName);
        }

    }
}