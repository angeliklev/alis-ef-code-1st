using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisFirst.Models
{
    public class AssetCondition
    {
        public int AssetConditionID { get; set; }
        public string Description { get; set; }
        public DateTime? IssuedDate { get; set; }
        public int AssetID { get; set; }

        public virtual Asset Asset { get; set; }
    }
}