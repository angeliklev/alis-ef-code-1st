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
    public class RepairController : Controller
    {
        private AliseEFDBContext db = new AliseEFDBContext();

        //
        // GET: /Repair/

        public ViewResult Index()
        {
            var repairs = db.Repairs.Include(r => r.Asset);
            return View(repairs.ToList());
        }

        //
        // GET: /Repair/Details/5

        public ViewResult Details(int id)
        {
            Repair repair = db.Repairs.Find(id);
            return View(repair);
        }

        //
        // GET: /Repair/Create

        public ActionResult Create()
        {
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode");
            return View();
        } 

        //
        // POST: /Repair/Create

        [HttpPost]
        public ActionResult Create(Repair repair)
        {
            if (ModelState.IsValid)
            {
                db.Repairs.Add(repair);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", repair.AssetID);
            return View(repair);
        }
        
        //
        // GET: /Repair/Edit/5
 
        public ActionResult Edit(int id)
        {
            Repair repair = db.Repairs.Find(id);
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", repair.AssetID);
            return View(repair);
        }

        //
        // POST: /Repair/Edit/5

        [HttpPost]
        public ActionResult Edit(Repair repair)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repair).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", repair.AssetID);
            return View(repair);
        }

        //
        // GET: /Repair/Delete/5
 
        public ActionResult Delete(int id)
        {
            Repair repair = db.Repairs.Find(id);
            return View(repair);
        }

        //
        // POST: /Repair/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Repair repair = db.Repairs.Find(id);
            db.Repairs.Remove(repair);
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