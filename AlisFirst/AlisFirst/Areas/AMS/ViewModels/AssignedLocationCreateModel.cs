using AlisFirst.DAL;
using AlisFirst.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssignedLocationCreateModel
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Assigned Date")]
        public DateTime AssignedLocationDate { get; set; }

        [Required]
        public int? LocationID { get; set; }

        public virtual Location Location { get; set; }
        public SelectList LocationsList { get; set; }

        // repo should not be here, better to move it with its call to a helper class!!
        private readonly ILocationRepository locationRepo = new LocationRepository();

        public AssignedLocationCreateModel() { }

        public AssignedLocationCreateModel(int id)
        {
            AssetID = id;
            AssignedLocationDate = DateTime.Today;
            LocationsList = new SelectList(locationRepo.All.ToList(), "LocationID", "LocationName", String.Empty);
        }
    }
    public class AssetAssignedLocationsModel
    {
        // repo should not be here, better to move it with its call to a helper class!!
        private readonly IAssignedLocationRepository assignedlocRepo = new AssignedLocationRepository();
        private readonly ILocationRepository locationRepo = new LocationRepository();

        public AssignedLocationCreateModel LocationToCreate { get; set; }
        public IEnumerable<LocationHistoryItemsModel> LocationHistory { get; set; }

        // constructors
        public AssetAssignedLocationsModel() { }
        public AssetAssignedLocationsModel(int id )
        {
            LocationToCreate = new AssignedLocationCreateModel(id);
            LocationHistory = AutoMapper.Mapper.Map<IEnumerable<AssignedLocation>,
                 IEnumerable<LocationHistoryItemsModel>>
                 (assignedlocRepo.GetLocHistory(id)); 
        }
        public AssetAssignedLocationsModel(AssignedLocationCreateModel validatedModel)
        {
            LocationToCreate = validatedModel;
            LocationToCreate.LocationsList = new SelectList(locationRepo.All.ToList(), "LocationID", "LocationName", String.Empty);
            LocationHistory = AutoMapper.Mapper.Map<IEnumerable<AssignedLocation>,
                 IEnumerable<LocationHistoryItemsModel>>
                 (assignedlocRepo.GetLocHistory(validatedModel.AssetID)); 
        }
    }

    public class LocationHistoryItemsModel
    {
        public string AssignedLocationDate { get; set; }
        public string LocationLocationName { get; set; }
    }
}