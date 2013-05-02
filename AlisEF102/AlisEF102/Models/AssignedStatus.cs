using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisEF102.Models
{
    public class AssignedStatus
    {
        public int AssignedStatusID { get; set; }
        public DateTime? AssignedDate { get; set; }
        public int AssetID  { get; set; }
        public int AssetStatusID { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual AssetStatus AssetStatus { get; set; }
    }
}