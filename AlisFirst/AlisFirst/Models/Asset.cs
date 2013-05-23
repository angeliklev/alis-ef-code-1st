using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AlisFirst.Models
{
    public class Asset
    {
        public Asset()
        {
            CheckListItems = new List<CheckListItem>();
            IsActive = true;
        }

        public int AssetID { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Bar code is required.")]
        public string BarCode { get; set; }

        [MaxLength(15)]
        [Display(Name = "Serial Number")]
        public string SerialNum { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [Display(Name = "Model")]
        public int AssetModelID { get; set; }
        public virtual AssetModel AssetModel { get; set; }

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
        public bool IsActive { get; set; }

        public virtual ICollection<AssetCondition> AssetConditions { get; set; }
        public virtual ICollection<CheckListItem> CheckListItems { get; set; }
        public virtual ICollection<Repair> Repairs { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<AssignedTo> AssignedToes { get; set; }
        public virtual ICollection<AssignedLocation> AssignedLocations { get; set; }

        //public string Name
        //{
        //    get { return string.Format("{0} {1} {2}", AssetModel.Manufacturer.ToString(), Category.ToString(), AssetModel.ToString()); }
        //}   
        public string Name
        {
            get { return string.Format("{0} {1} {2}", AssetModel.Manufacturer, Category, AssetModel); }
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}