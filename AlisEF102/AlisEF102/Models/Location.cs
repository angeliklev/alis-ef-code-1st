using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisEF102.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<AssignedLocation> AssignedLocations { get; set; }
    }
}