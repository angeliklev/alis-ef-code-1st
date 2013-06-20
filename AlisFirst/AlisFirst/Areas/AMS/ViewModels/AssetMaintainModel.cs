// File /ViewModels/AssetMaintainModel.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using AlisFirst.DAL;
using AlisFirst.Models;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetMaintainModel
    {
        public AssetEditModel AssetToEdit { get; set; }
        public AssetAssignedLocationsModel AssetLocations { get; set; }
        public AssetAssignedStatusModel AssetAssignedStatus { get; set; }
        public AssetRepairsModel AssetRepairs { get; set; }
        public AssetConditionsModel AssetConditions { get; set; }
        public AssetCheckListEditModel AssetCheckListView { get; set; }

        private readonly IAssetRepository assetRepo = new AssetRepository();

        public AssetMaintainModel() { }
        public AssetMaintainModel(int id)
        {
            var assetToEdit = assetRepo.Find(id);
            this.AssetToEdit = AutoMapper.Mapper.Map<Asset, AssetEditModel>(assetToEdit);

            this.AssetLocations = new AssetAssignedLocationsModel(id);
            this.AssetAssignedStatus = new AssetAssignedStatusModel(id);
            this.AssetRepairs = new AssetRepairsModel(id);
            this.AssetConditions = new AssetConditionsModel(id);
            this.AssetCheckListView = new AssetCheckListEditModel(id);
        }
    }
}

