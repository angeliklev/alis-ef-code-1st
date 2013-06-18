using AlisFirst.DAL;
using AlisFirst.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssignedStatusCreateModel
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Assigned Date")]
        public DateTime? AssignedDate { get; set; }

        [Required]
        public int? AssetStatusID { get; set; }

        public virtual AssetStatus AssetStatus { get; set; }
        public SelectList StatusesList { get; set; }

        private readonly IAssetStatusRepository assetstatusesRepo = new AssetStatusRepository();
        public AssignedStatusCreateModel() { }

        public AssignedStatusCreateModel(int id)
        {
            AssetID = id;
            AssignedDate = DateTime.Now;
            StatusesList = new SelectList(assetstatusesRepo.All.ToList(), "AssetStatusID", "AssetStatusName", String.Empty);
        }
    }

    public class AssetAssignedStatusModel
    {
        private readonly IAssignedStatusRepository assignedstatusRepo = new AssignedStatusRepository();
        private readonly IAssetStatusRepository assetstatusRepo = new AssetStatusRepository();

        public AssignedStatusCreateModel StatusToCreate { get; set; }
        public IEnumerable<AssetStatusHistoryModel> StatusHistory { get; set; }

        public AssetAssignedStatusModel() { }
        public AssetAssignedStatusModel(int id)
        {
            StatusToCreate = new AssignedStatusCreateModel(id);
            StatusHistory = Mapper.Map<IEnumerable<AssignedStatus>, IEnumerable<AssetStatusHistoryModel>>
                (assignedstatusRepo.All.OrderBy(l => l.AssignedDate).Where(m => m.AssetID == id));
        }
        public AssetAssignedStatusModel(AssignedStatusCreateModel validatedModel)
        {
            StatusToCreate = validatedModel;
            StatusToCreate.StatusesList = new SelectList(assetstatusRepo.All.ToList(), "AssetStatusID", "AssetStatusName", String.Empty);
            StatusHistory = Mapper.Map<IEnumerable<AssignedStatus>, IEnumerable<AssetStatusHistoryModel>>
                (assignedstatusRepo.All.OrderBy(l => l.AssignedDate).Where(m => m.AssetID == validatedModel.AssetID));
        }
    }

    public class AssetStatusHistoryModel
    {
        public string AssignedDate { get; set; }
        public string AssetStatusAssetStatusName { get; set; }
    }
}