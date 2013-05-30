using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetMaintain
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }
        public AssetEdit AssetToEdit { get; set; }
        public AssetRepairsEdit AssetRepairs { get; set; }
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

    public class AssetRepairsEdit
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; } // I have concerns as for holding this attribute here

        public AssetRepair RepairToCreate { get; set; }
        public IEnumerable<AssetRepair> RepairHistory { get; set; }

        public AssetRepairsEdit()
        {
            // actually this never should happen... create a message?
            this.RepairHistory = null;
            this.RepairToCreate = null;
        }

        public AssetRepairsEdit(int id)
        {
            // I am not sure repository and query really should be here 
            // but wanted to move the constructor code from the controller
            IRepairRepository repairRepo = new RepairRepository();
            var repairs = repairRepo.All.Where(m => m.AssetID == id);

            this.RepairHistory = AutoMapper.Mapper.Map<IEnumerable<Repair>,
                IEnumerable<AssetRepairsEdit.AssetRepair>>(repairs);
            this.RepairToCreate = new AssetRepair(id);
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
            //[ScaffoldColumn(false)] //cannot turn this attribute off. 
            // must have AssetID on the form to be able to save data to database
            // or have to invent something
            public int AssetID { get; set; }

            // not sure about default constructor...
            public AssetRepair () { }

            public AssetRepair(int id)
            {
                AssetID = id;
                IssuedDate = DateTime.Today;
                TechnicianName = "";
                Result = " ";
            }
        }
    }
}