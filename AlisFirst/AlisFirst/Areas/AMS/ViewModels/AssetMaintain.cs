// File /ViewModels/AssetMaintain.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetMaintain
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }
        public AssetEdit AssetToEdit { get; set; }
        public AssetRepairs AssetRepairs { get; set; }
    }

    public class AssetEdit
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        public string BarCode { get; set; }

        [MaxLength(15)]
        [Display(Name = "Serial Number")]
        public string SerialNum { get; set; }
    }

    public class AssetRepairs
    {
        // We might need it when rendering partial view, then we must know AssetID
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; } 

        public AssetRepair RepairToCreate { get; set; }
        public IEnumerable<AssetRepair> RepairHistory { get; set; }
    }

    public class AssetRepair
    {
        [Required]
        public DateTime IssuedDate { get; set; }
        [Required]
        public string TechnicianName { get; set; }
        [Required]
        public string Result { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }
    }
}