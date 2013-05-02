using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisEF102.Models;
using AlisEF102.DAL;

namespace AlisEF102.Controllers
{ 
    public class AssignedLocationController : Controller
    {
        private AliseEFDBContext db = new AliseEFDBContext();

        //
        // GET: /AssignedLocation/

        public ViewResult Index()
        {
            var assignedlocations = db.AssignedLocations.Include(a => a.Asset).Include(a => a.Location);
            return View(assignedlocations.ToList());
        }

        //
        // GET: /AssignedLocation/Details/5

        public ViewResult Details(int id)
        {
            AssignedLocation assignedlocation = db.AssignedLocations.Find(id);
            return View(assignedlocation);
        }

        //
        // GET: /AssignedLocation/Create

        public ActionResult Create()
        {
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode");
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
            return View();
        } 

        //
        // POST: /AssignedLocation/Create

        [HttpPost]
        public ActionResult Create(AssignedLocation assignedlocation)
        {
            if (ModelState.IsValid)
            {
                db.AssignedLocations.Add(assignedlocation);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", assignedlocation.AssetID);
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", assignedlocation.LocationID);
            return View(assignedlocation);
        }
        
        //
        // GET: /AssignedLocation/Edit/5
 
        public ActionResult Edit(int id)
        {
            AssignedLocation assignedlocation = db.AssignedLocations.Find(id);
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", assignedlocation.AssetID);
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", assignedlocation.LocationID);
            return View(assignedlocation);
        }

        //
        // POST: /AssignedLocation/Edit/5

        [HttpPost]
        public ActionResult Edit(AssignedLocation assignedlocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignedlocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", assignedlocation.AssetID);
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", assignedlocation.LocationID);
            return View(assignedlocation);
        }

        //
        // GET: /AssignedLocation/Delete/5
 
        public ActionResult Delete(int id)
        {
            AssignedLocation assignedlocation = db.AssignedLocations.Find(id);
            return View(assignedlocation);
        }

        //
        // POST: /AssignedLocation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            AssignedLocation assignedlocation = db.AssignedLocations.Find(id);
            db.AssignedLocations.Remove(assignedlocation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}