using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlisFirst.ViewModels
{
    public class ListManufacturerViewModel
    {
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
    }
    public class EditManufacturerViewModel
    {
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
    }
    public class CreateManufacturerViewModel
    {
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
    }
    public class DeleteManufacturerViewModel
    {
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
    }
}