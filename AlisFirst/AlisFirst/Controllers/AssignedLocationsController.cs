using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.Areas
{   
    public class AssignedLocationsController : Controller
    {
		private readonly IAssetRepository assetRepository;
		private readonly ILocationRepository locationRepository;
		private readonly IAssignedLocationRepository assignedlocationRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AssignedLocationsController() : this(new AssetRepository(), new LocationRepository(), new AssignedLocationRepository())
        {
        }

        public AssignedLocationsController(IAssetRepository assetRepository, ILocationRepository locationRepository, IAssignedLocationRepository assignedlocationRepository)
        {
			this.assetRepository = assetRepository;
			this.locationRepository = locationRepository;
			this.assignedlocationRepository = assignedlocationRepository;
        }

        //
        // GET: /AssignedLocations/

        public ViewResult Index()
        {
            return View(assignedlocationRepository.AllIncluding(assignedlocation => assignedlocation.Asset, assignedlocation => assignedlocation.Location));
        }

        //
        // GET: /AssignedLocations/Details/5

        public ViewResult Details(int id)
        {
            return View(assignedlocationRepository.Find(id));
        }

        //
        // GET: /AssignedLocations/Create

        public ActionResult Create()
        {
			ViewBag.PossibleAssets = assetRepository.All;
			ViewBag.PossibleLocations = locationRepository.All;
            return View();
        } 

        //
        // POST: /AssignedLocations/Create

        [HttpPost]
        public ActionResult Create(AssignedLocation assignedlocation)
        {
            if (ModelState.IsValid) {
                assignedlocationRepository.InsertOrUpdate(assignedlocation);
                assignedlocationRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAssets = assetRepository.All;
				ViewBag.PossibleLocations = locationRepository.All;
				return View();
			}
        }
        
        //
        // GET: /AssignedLocations/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleAssets = assetRepository.All;
			ViewBag.PossibleLocations = locationRepository.All;
             return View(assignedlocationRepository.Find(id));
        }

        //
        // POST: /AssignedLocations/Edit/5

        [HttpPost]
        public ActionResult Edit(AssignedLocation assignedlocation)
        {
            if (ModelState.IsValid) {
                assignedlocationRepository.InsertOrUpdate(assignedlocation);
                assignedlocationRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAssets = assetRepository.All;
				ViewBag.PossibleLocations = locationRepository.All;
				return View();
			}
        }

        //
        // GET: /AssignedLocations/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(assignedlocationRepository.Find(id));
        }

        //
        // POST: /AssignedLocations/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            assignedlocationRepository.Delete(id);
            assignedlocationRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                assetRepository.Dispose();
                locationRepository.Dispose();
                assignedlocationRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

