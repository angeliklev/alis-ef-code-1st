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
    public class AssetModelController : Controller
    {
        private AliseEFDBContext db = new AliseEFDBContext();

        //
        // GET: /AssetModel/

        public ViewResult Index()
        {
            var assetmodels = db.AssetModels.Include(a => a.Manufacturer);
            return View(assetmodels.ToList());
        }

        //
        // GET: /AssetModel/Details/5

        public ViewResult Details(int id)
        {
            AssetModel assetmodel = db.AssetModels.Find(id);
            return View(assetmodel);
        }

        //
        // GET: /AssetModel/Create

        public ActionResult Create()
        {
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName");
            return View();
        } 

        //
        // POST: /AssetModel/Create

        [HttpPost]
        public ActionResult Create(AssetModel assetmodel)
        {
            if (ModelState.IsValid)
            {
                db.AssetModels.Add(assetmodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName", assetmodel.ManufacturerID);
            return View(assetmodel);
        }
        
        //
        // GET: /AssetModel/Edit/5
 
        public ActionResult Edit(int id)
        {
            AssetModel assetmodel = db.AssetModels.Find(id);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName", assetmodel.ManufacturerID);
            return View(assetmodel);
        }

        //
        // POST: /AssetModel/Edit/5

        [HttpPost]
        public ActionResult Edit(AssetModel assetmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName", assetmodel.ManufacturerID);
            return View(assetmodel);
        }

        //
        // GET: /AssetModel/Delete/5
 
        public ActionResult Delete(int id)
        {
            AssetModel assetmodel = db.AssetModels.Find(id);
            return View(assetmodel);
        }

        //
        // POST: /AssetModel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            AssetModel assetmodel = db.AssetModels.Find(id);
            db.AssetModels.Remove(assetmodel);
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