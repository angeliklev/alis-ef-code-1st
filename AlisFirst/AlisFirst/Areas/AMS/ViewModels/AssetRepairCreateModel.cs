using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using AlisFirst.DAL;
using AlisFirst.Models;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetRepairCreateModel
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date ")]
        public DateTime IssuedDate { get; set; }

        [Required]
        [Display(Name = "Technician Name ")]
        public string TechnicianName { get; set; }
        [Required]
        public string Result { get; set; }

        public AssetRepairCreateModel() { }
        public AssetRepairCreateModel(int id)
        {
            AssetID = id;
            IssuedDate = DateTime.Today;
        }
    }
    public class AssetRepairsModel
    {
        public AssetRepairCreateModel RepairToCreate { get; set; }
        public IEnumerable<AssetRepairsHistoryModel> RepairHistory { get; set; }

        // repo should not be here, better to move it with its call to a helper class!!
        private readonly IRepairRepository repairRepo = new RepairRepository();

        private AssetRepairsModel() { }

        public AssetRepairsModel(int id)
        {
            RepairToCreate = new AssetRepairCreateModel(id);
            RepairHistory = AutoMapper.Mapper.Map<IEnumerable<Repair>,
                    IEnumerable<AssetRepairsHistoryModel>>
                    (repairRepo.All.OrderBy(l => l.IssuedDate).Where(m => m.AssetID == id));
        }
        public AssetRepairsModel(AssetRepairCreateModel validatedModel)
        {
            RepairToCreate = validatedModel;
            RepairHistory = AutoMapper.Mapper.Map<IEnumerable<Repair>,
                    IEnumerable<AssetRepairsHistoryModel>>
                    (repairRepo.All.OrderBy(l => l.IssuedDate).Where(m => m.AssetID == validatedModel.AssetID));
        }
    }
    public class AssetRepairsHistoryModel
    {
        public string IssuedDate { get; set; }
        public string TechnicianName { get; set; }
        public string Result { get; set; }
    }
}