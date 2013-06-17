using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AlisFirst.Areas.AMS.ViewModels;
using AlisFirst.DAL;
using AlisFirst.Models;

namespace AlisFirst.Areas.AMS.Controllers
{   
    public class AssignedLocationController : Controller
    {
        private readonly ILocationRepository locationRepo;
		private readonly IAssignedLocationRepository assignedlocRepo;

		// If you are using Dependency Injection, you can delete the following constructor
        public AssignedLocationController() : this(new LocationRepository(),          
            new AssignedLocationRepository())
        {
        }

        public AssignedLocationController(ILocationRepository locationRepository, 
            IAssignedLocationRepository assignedlocationRepository)
        {
            this.locationRepo = locationRepository;
			this.assignedlocRepo = assignedlocationRepository;
        }

        // 
        // POST from partial _AssetLocationCreate
        
        [HttpPost]
        public ActionResult Create(AssignedLocationCreateModel assignedLocation)
        {
            if (ModelState.IsValid)
            {
                assignedlocRepo.InsertOrUpdate(AutoMapper.Mapper.Map<AssignedLocationCreateModel,
                                                                AssignedLocation>(assignedLocation));
                assignedlocRepo.Save();
                // assigned locations list changed!
                var locationsList = AutoMapper.Mapper.Map<IEnumerable<AssignedLocation>,
                    IEnumerable<AssetMaintainModel.LocationHistoryItemsModel>>(assignedlocRepo
                    .AllIncluding(l => l.Location).OrderBy(l => l.AssignedLocationDate)
                    .Where(m => m.AssetID == assignedLocation.AssetID));
                if (Request.IsAjaxRequest())
                {
                    var viewModel = new AssetMaintainModel.AssetAssignedLocationsModel(assignedLocation.AssetID,
                        locationRepo.All.ToList(), locationsList);
                    return PartialView("_Create", viewModel);
                }
                else
                {
                    // this returns the whole page again, with updated data
                    return RedirectToAction("Edit", new { id = assignedLocation.AssetID });
                }
            }
            else
            {
                // assigned locations did not change
                var locationsList = AutoMapper.Mapper.Map<IEnumerable<AssignedLocation>,
                    IEnumerable<AssetMaintainModel.LocationHistoryItemsModel>>(assignedlocRepo
                    .AllIncluding(l => l.Location).OrderBy(l => l.AssignedLocationDate)
                    .Where(m => m.AssetID == assignedLocation.AssetID));
                if (Request.IsAjaxRequest())
                {
                    var viewModel = new AssetMaintainModel.AssetAssignedLocationsModel
                    {
                        LocationHistory = locationsList,
                        LocationToCreate = new AssignedLocationCreateModel(assignedLocation.AssetID, locationRepo.All.ToList())
                    };
                    return PartialView("_Create", viewModel);
                }
                else
                {
                    return RedirectToAction("../Asset/Edit/", assignedLocation.AssetID);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                locationRepo.Dispose();
                assignedlocRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

