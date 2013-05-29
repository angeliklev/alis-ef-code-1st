using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetMaintain
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }
        public AssetEdit AssetToEdit { get; set; }
        public AssetRepairHistory AssetRepairs { get; set; }
    }

    public class AssetEdit
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        public string BarCode { get; set; }

        //public string Name { get; set; }

        [MaxLength(15)]
        [Display(Name = "Serial Number")]
        public string SerialNum { get; set; }
    }

    public class AssetRepairHistory
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        public IEnumerable<AssetRepair> RepairHistory { get; set; }
        public AssetRepair RepairToCreate { get; set; }

        public class AssetRepair
        {
            public DateTime? IssuedDate { get; set; }
            public string TechnicianName { get; set; }
            public string Result { get; set; }

            [HiddenInput(DisplayValue = false)]
            public int AssetID { get; set; }
        }
    }

}