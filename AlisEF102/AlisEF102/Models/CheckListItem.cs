using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisEF102.Models
{
    public class CheckListItem
    {
        public int CheckListItemID { get; set; }
        public string CheckListItemName { get; set; }
        public int AssetID { get; set; }

        public virtual Asset Asset { get; set; }
    }
}