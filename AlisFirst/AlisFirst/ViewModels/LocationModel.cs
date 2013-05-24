using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlisFirst.ViewModels
{
    public class ListLocationViewModel
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }
    public class EditLocationViewModel
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }
    public class CreateLocationViewModel
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }
    public class DeleteLocationViewModel
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }
}