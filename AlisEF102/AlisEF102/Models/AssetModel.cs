using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisEF102.Models
{
    public class AssetModel
    {
        public AssetModel()
        {
            Assets = new List<Asset>();
        }

        public int AssetModelID { get; set; }
        public string AssetModelName { get; set; }

        public int ManufacturerID { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", AssetModelName);
        }
        
    }
}