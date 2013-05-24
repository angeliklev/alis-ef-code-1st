using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlisFirst.ViewModels
{
    public class ListSupplierViewModel
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
    }
    public class EditSupplierViewModel
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
    }
    public class CreateSupplierViewModel
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
    }
    public class DeleteSupplierViewModel
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
    }
}