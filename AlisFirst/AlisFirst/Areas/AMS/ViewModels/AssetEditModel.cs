using AlisFirst.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetEditModel
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Bar code is required.")]
        public string BarCode { get; set; }

        [MaxLength(15)]
        [Display(Name = "Serial Number")]
        public string SerialNum { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        //public virtual Supplier Supplier { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        //public virtual Category Category { get; set; }

        [Display(Name = "Model")]
        public int AssetModelID { get; set; }
        //public virtual AssetModel AssetModel { get; set; }


        [Display(Name = "Date Recieved")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DateRecieved { get; set; }

        [Display(Name = "Warranty Expires")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? WarrantyExpires { get; set; }

        public decimal? PurchaseCost { get; set; }

        [Display(Name = "Loanable")]
        public bool IsLoanable { get; set; }

        public string Notes { get; set; }

        [ScaffoldColumn(false)]
        public string Name { get; set; }

        public AssetEditModel() { }

        public AssetEditModel(int id) 
        {
        }

    }
}