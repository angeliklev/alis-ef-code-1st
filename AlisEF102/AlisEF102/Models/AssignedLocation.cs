using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisEF102.Models
{
    public class AssignedLocation
    {
        public int AssignedLocationID { get; set; }
        public DateTime? AssignedLocationDate { get; set; }
        public int AssetID { get; set; }
        public int LocationID { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Location Location { get; set; }
    }
}