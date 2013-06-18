using AlisFirst.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetEditModel
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        public string BarCode { get; set; }

        [MaxLength(15)]
        [Display(Name = "Serial Number")]
        public string SerialNum { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [Display(Name = "Model")]
        public int AssetModelID { get; set; }
        public virtual AssetModel AssetModel { get; set; }

        [ScaffoldColumn(false)]
        public string Name { get; set; }

        public AssetEditModel() { }

        public AssetEditModel(int id) 
        {
        }

    }
}