// File /ViewModels/AssetMaintain.cs
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AlisFirst.Models;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetMaintain
    {
        public AssetEditVM AssetToEdit { get; set; }
        public AssetRepairsVM AssetRepairs { get; set; }
        public AssetAssignedLocationsVM AssetLocations { get; set; }
        public AssetCheckListVM AssetCheckListView { get; set; }
    }

    public class AssetEditVM
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        public string BarCode { get; set; }

        [MaxLength(15)]
        [Display(Name = "Serial Number")]
        public string SerialNum { get; set; }
    }

    public class AssetRepairsVM
    {
        public CreateAssetRepairVM RepairToCreate { get; set; }
        public IEnumerable<CreateAssetRepairVM> RepairHistory { get; set; }
    }

    public class CreateAssetRepairVM
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime IssuedDate { get; set; }
        [Required]
        public string TechnicianName { get; set; }
        [Required]
        public string Result { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }
    }

    public class AssetAssignedLocationsVM
    {
        public CreateAssignedLocationWithDDLVM LocationToCreate { get; set; }
        public IEnumerable<LocationHistoryItemsVM> LocationHistory { get; set; }
    }

    public class LocationHistoryItemsVM
    {
        public string AssignedLocationDate { get; set; }
        public string LocationLocationName { get; set; }
    }

    public class CreateAssignedLocationWithDDLVM
    {
        public CreateAssignedLocationVM AssignedLocation { get; set; }
        public SelectList LocationsList { get; private set; }

        public CreateAssignedLocationWithDDLVM(CreateAssignedLocationVM newlocation,
                                        IEnumerable locations)
        {
            AssignedLocation = newlocation;
            LocationsList = new SelectList(locations, "LocationID", "LocationName", String.Empty);
        }
    }

    public class CreateAssignedLocationVM
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

