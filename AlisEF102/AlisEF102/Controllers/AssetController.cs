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
    public class AssetController : Controller
    {
        private AliseEFDBContext db = new AliseEFDBContext();

        //
        // GET: /Asset/

        public ViewResult Index()
        {
            var assets = db.Assets.Include(a => a.Supplier).Include(a => a.AssetModel);
            return View(assets.ToList());
        }

        //
        // GET: /Asset/Details/5

        public ViewResult Details(int id)
        {
            Asset asset = db.Assets.Find(id);
            return View(asset);
        }

        //
        // GET: /Asset/Create

        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
            ViewBag.AssetModelID = new SelectList(db.AssetModels, "AssetModelID", "AssetModelName");
            return View();
        } 

        //
        // POST: /Asset/Create

        [HttpPost]
        public ActionResult Create(Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Assets.Add(asset);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", asset.SupplierID);
            ViewBag.AssetModelID = new SelectList(db.AssetModels, "AssetModelID", "AssetModelName", asset.AssetModelID);
            return View(asset);
        }
        
        //
        // GET: /Asset/Edit/5
 
        public ActionResult Edit(int id)
        {
            Asset asset = db.Assets.Find(id);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", asset.SupplierID);
            ViewBag.AssetModelID = new SelectList(db.AssetModels, "AssetModelID", "AssetModelName", asset.AssetModelID);
            return View(asset);
        }

        //
        // POST: /Asset/Edit/5

        [HttpPost]
        public ActionResult Edit(Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", asset.SupplierID);
            ViewBag.AssetModelID = new SelectList(db.AssetModels, "AssetModelID", "AssetModelName", asset.AssetModelID);
            return View(asset);
        }

        //
        // GET: /Asset/Delete/5
 
        public ActionResult Delete(int id)
        {
            Asset asset = db.Assets.Find(id);
            return View(asset);
        }

        //
        // POST: /Asset/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Asset asset = db.Assets.Find(id);
            db.Assets.Remove(asset);
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