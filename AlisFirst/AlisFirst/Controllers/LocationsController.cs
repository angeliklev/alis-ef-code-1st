using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.Controllers
{   
    public class LocationsController : Controller
    {
		private readonly ILocationRepository locationRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public LocationsController() : this(new LocationRepository())
        {
        }

        public LocationsController(ILocationRepository locationRepository)
        {
			this.locationRepository = locationRepository;
        }

        //
        // GET: /Locations/

        public ViewResult Index()
        {
            return View(locationRepository.AllIncluding(location => location.AssignedLocations));
        }

        //
        // GET: /Locations/Details/5

        public ViewResult Details(int id)
        {
            return View(locationRepository.Find(id));
        }

        //
        // GET: /Locations/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Locations/Create

        [HttpPost]
        public ActionResult Create(Location location)
        {
            if (ModelState.IsValid) {
                locationRepository.InsertOrUpdate(location);
                locationRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Locations/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(locationRepository.Find(id));
        }

        //
        // POST: /Locations/Edit/5

        [HttpPost]
        public ActionResult Edit(Location location)
        {
            if (ModelState.IsValid) {
                locationRepository.InsertOrUpdate(location);
                locationRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Locations/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(locationRepository.Find(id));
        }

        //
        // POST: /Locations/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            locationRepository.Delete(id);
            locationRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                locationRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

