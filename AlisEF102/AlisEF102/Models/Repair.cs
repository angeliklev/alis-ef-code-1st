using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisEF102.Models
{
    public class Repair
    {
        public int RepairID { get; set; }
        public DateTime? IssuedDate { get; set; }
        public string TechnicianName { get; set; }
        public string Result { get; set; }
        public int AssetID { get; set; }

        public virtual Asset Asset { get; set; }
    }
}