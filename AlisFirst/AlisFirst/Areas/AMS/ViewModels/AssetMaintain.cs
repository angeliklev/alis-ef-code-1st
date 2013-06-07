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
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }
        public AssetEdit AssetToEdit { get; set; }
        public AssetRepairs AssetRepairs { get; set; }
        public AssetAssignedLocationsVM AssetLocations { get; set; }
        public AssetCheckListVM AssetCheckListView { get; set; }
    }

    public class AssetEdit
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        public string BarCode { get; set; }

        [MaxLength(15)]
        [Display(Name = "Serial Number")]
        public string SerialNum { get; set; }
    }

    public class AssetRepairs
    {
        // We might need it when rendering partial view, then we must know AssetID
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; } 

        public AssetRepair RepairToCreate { get; set; }
        public IEnumerable<AssetRepair> RepairHistory { get; set; }
    }

    public class AssetRepair
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
        public AssignedLocationVM LocationToCreate { get; set; }
        public IEnumerable<AssignedLocationData> LocationHistory { get; set; }
    }

    public class AssignedLocationData
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; } 
       
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AssignedLocationDate { get; set; }

        public int? LocationID { get; set; }
        public virtual Location Location { get; set; } 
    }

    public class AssignedLocationVM
    {
        public AssignedLocationData  AssignedLocation { get; set; }
        // list of locatins for DDL to select
        public SelectList Locations { get; private set; }

        public AssignedLocationVM(AssignedLocationData newlocation,  
                                        IEnumerable locations) {
            AssignedLocation = newlocation;
            Locations = new SelectList(locations, "LocationID", "LocationName", AssignedLocation.LocationID);
        }

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

