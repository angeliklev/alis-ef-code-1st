using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class SelectedCheckListItemsData
    {
        public int CheckListItemID { get; set; }
        public string ItemName { get; set; }
        public bool Selected { get; set; }
    }
}
