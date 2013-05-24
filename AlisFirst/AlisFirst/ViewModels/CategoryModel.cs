using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlisFirst.ViewModels
{
    public class ListCategoryViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class EditCategoryViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class CreateCategoryViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class DeleteCategoryViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}