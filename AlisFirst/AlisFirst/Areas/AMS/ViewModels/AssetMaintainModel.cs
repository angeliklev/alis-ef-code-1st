// File /ViewModels/AssetMaintain.cs
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
        }

        public class AssetAssignedLocationsModel
        {
            public AssignedLocationCreateDDLModel LocationToCreate { get; set; }
            public IEnumerable<LocationHistoryItemsModel> LocationHistory { get; set; }
        }

        public class LocationHistoryItemsModel
        {
            public string AssignedLocationDate { get; set; }
            public string LocationLocationName { get; set; }
        }

        public class AssignedLocationCreateDDLModel
        {
            public AssignedLocationCreateModel AssignedLocation { get; set; }
            public SelectList LocationsList { get; private set; }

            public AssignedLocationCreateDDLModel(AssignedLocationCreateModel newlocation,
                                            IEnumerable locations)
            {
                AssignedLocation = newlocation;
                LocationsList = new SelectList(locations, "LocationID", "LocationName", String.Empty);
            }
        }

        public class AssignedLocationCreateModel
        {
            [HiddenInput(DisplayValue = false)]
            public int AssetID { get; set; }

            [Required]
            [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            public DateTime AssignedLocationDate { get; set; }

            [Required]
            public int? LocationID { get; set; }
            public virtual Location Location { get; set; }
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

