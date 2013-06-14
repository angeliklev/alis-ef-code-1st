// File /ViewModels/AssetMaintainModel.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AlisFirst.Models;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetMaintainModel
    {
        public AssetEditVM AssetToEdit { get; set; }
        public AssetRepairsModel AssetRepairs { get; set; }
        public AssetAssignedLocationsModel AssetLocations { get; set; }
        public AssetCheckListVM AssetCheckListView { get; set; }

        public class AssetEditVM
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
            public string Name { get; set; }
        }
        public class AssetRepairsModel
        {
            public AssetRepairCreateModel RepairToCreate { get; set; }
            public IEnumerable<AssetRepairsHistoryModel> RepairHistory { get; set; }
        }
        public class AssetRepairsHistoryModel
        {
            public string IssuedDate { get; set; }
            public string TechnicianName { get; set; }
            public string Result { get; set; }
        }

        public class AssetAssignedLocationsModel
        {
            public AssignedLocationCreateModel LocationToCreate { get; set; }
            public IEnumerable<LocationHistoryItemsModel> LocationHistory { get; set; }

            public AssetAssignedLocationsModel() { }
            public AssetAssignedLocationsModel(int id, IEnumerable locations, 
                IEnumerable<AssetMaintainModel.LocationHistoryItemsModel> historylist) 
            { 
                LocationToCreate = new AssignedLocationCreateModel(id, locations ) ;
                LocationHistory = historylist;
            }
        }

        public class LocationHistoryItemsModel
        {
            public string AssignedLocationDate { get; set; }
            public string LocationLocationName { get; set; }
        }

        public class AssetCheckListVM
        {
            [HiddenInput(DisplayValue = false)]
            public int AssetID { get; set; }
            public List<SelectedCheckListItemsData> SelectedItems { get; set; }
        }

        public class SelectedCheckListItemsData
        {
            public int CheckListItemID { get; set; }
            public string ItemName { get; set; }
            public bool Selected { get; set; }
        }
    }
 }

