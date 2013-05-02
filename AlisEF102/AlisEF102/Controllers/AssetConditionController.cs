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
    public class AssetConditionController : Controller
    {
        private AliseEFDBContext db = new AliseEFDBContext();

        //
        // GET: /AssetCondition/

        public ViewResult Index()
        {
            var assetconditions = db.AssetConditions.Include(a => a.Asset);
            return View(assetconditions.ToList());
        }

        //
        // GET: /AssetCondition/Details/5

        public ViewResult Details(int id)
        {
            AssetCondition assetcondition = db.AssetConditions.Find(id);
            return View(assetcondition);
        }

        //
        // GET: /AssetCondition/Create

        public ActionResult Create()
        {
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode");
            return View();
        } 

        //
        // POST: /AssetCondition/Create

        [HttpPost]
        public ActionResult Create(AssetCondition assetcondition)
        {
            if (ModelState.IsValid)
            {
                db.AssetConditions.Add(assetcondition);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", assetcondition.AssetID);
            return View(assetcondition);
        }
        
        //
        // GET: /AssetCondition/Edit/5
 
        public ActionResult Edit(int id)
        {
            AssetCondition assetcondition = db.AssetConditions.Find(id);
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", assetcondition.AssetID);
            return View(assetcondition);
        }

        //
        // POST: /AssetCondition/Edit/5

        [HttpPost]
        public ActionResult Edit(AssetCondition assetcondition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetcondition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", assetcondition.AssetID);
            return View(assetcondition);
        }

        //
        // GET: /AssetCondition/Delete/5
 
        public ActionResult Delete(int id)
        {
            AssetCondition assetcondition = db.AssetConditions.Find(id);
            return View(assetcondition);
        }

        //
        // POST: /AssetCondition/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            AssetCondition assetcondition = db.AssetConditions.Find(id);
            db.AssetConditions.Remove(assetcondition);
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