using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisFirst.Models
{
    public class CheckListItem
    {
       public CheckListItem()
       {
           Assets = new List<Asset>();
       }

        public int CheckListItemID { get; set; }
        public string CheckListItemName { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}