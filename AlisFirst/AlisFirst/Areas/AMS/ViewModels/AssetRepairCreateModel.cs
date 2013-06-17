using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetRepairCreateModel
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime IssuedDate { get; set; }
        [Required]
        public string TechnicianName { get; set; }
        [Required]
        public string Result { get; set; }

        public AssetRepairCreateModel() 
        { 
            //maybe should create message that this object cannot be created without id parameter
        }
        public AssetRepairCreateModel(int id)
        {
            AssetID = id;
            IssuedDate = DateTime.Today;
        }
    }
}