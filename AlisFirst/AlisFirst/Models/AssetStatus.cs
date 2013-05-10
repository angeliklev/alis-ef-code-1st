using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisFirst.Models
{
    public class AssetStatus
    {
        public int AssetStatusID { get; set; }
        public string AssetStatusName { get; set; }

        public ICollection<AssignedStatus> AssignedStatuses { get; set; }
    }
}