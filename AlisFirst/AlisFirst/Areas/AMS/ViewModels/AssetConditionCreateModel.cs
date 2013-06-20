using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using AlisFirst.DAL;
using AlisFirst.Models;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetConditionCreateModel
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Issued Date")]
        public DateTime IssuedDate { get; set; }
        [Required]
        public string Description { get; set; }

        public AssetConditionCreateModel() { }
        public AssetConditionCreateModel(int id)
        {
            AssetID = id;
            IssuedDate = DateTime.Today;
        }
    }

    public class AssetConditionsModel
    {
        public AssetConditionCreateModel ConditionToCreate { get; set; }
        public IEnumerable<AssetConditionHistoryModel> ConditionHistory { get; set; }

        private readonly IAssetConditionRepository condRepo = new AssetConditionRepository();

        public AssetConditionsModel() { }

        public AssetConditionsModel(int id) 
        {
            ConditionToCreate = new AssetConditionCreateModel(id);
            ConditionHistory = AutoMapper.Mapper.Map<IEnumerable<AssetCondition>, IEnumerable<AssetConditionHistoryModel>>
                (condRepo.All.OrderBy(l => l.IssuedDate).Where(m => m.AssetID == id));
        }
        public AssetConditionsModel(AssetConditionCreateModel validatedModel)
        {
            ConditionToCreate = validatedModel;
            ConditionHistory = AutoMapper.Mapper.Map<IEnumerable<AssetCondition>, IEnumerable<AssetConditionHistoryModel>>
                (condRepo.All.OrderBy(l => l.IssuedDate).Where(m => m.AssetID == validatedModel.AssetID));
        }
    }

    public class AssetConditionHistoryModel
    {
        public string IssuedDate { get; set; }
        public string Description { get; set; }
    }
}