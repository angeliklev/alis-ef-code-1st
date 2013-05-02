using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AlisEF102.Models
{
    public class Asset
    {
        public int AssetID { get; set; }
        public string BarCode { get; set; }
        public string SerialNum { get; set; }
        public DateTime? DateRecieved { get; set; }
        public bool IsActive { get; set; }
        public DateTime? WarrantyExpires { get; set; }
        public decimal? PurchaseCost { get; set; }
        public bool IsLoanable { get; set; }
        public string Notes { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public int SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }

        public int AssetModelID { get; set; }
        public virtual AssetModel AssetModel { get; set; }

        public virtual ICollection<AssetCondition> AssetConditions { get; set; }
        public virtual ICollection<CheckListItem> CheckListItems { get; set; }
        public virtual ICollection<Repair> Repairs { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<AssignedTo> AssignedToes { get; set; }

        public string AssetName
        {
            get { return string.Format("{0} {1} {2}", AssetModel.Manufacturer.ToString(), Category.ToString(), AssetModel.ToString() ); }
        }
      
    }
}