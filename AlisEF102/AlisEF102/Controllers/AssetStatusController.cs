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
    public class AssetStatusController : Controller
    {
        private AliseEFDBContext db = new AliseEFDBContext();

        //
        // GET: /AssetStatus/

        public ViewResult Index()
        {
            return View(db.AssetStatus.ToList());
        }

        //
        // GET: /AssetStatus/Details/5

        public ViewResult Details(int id)
        {
            AssetStatus assetstatus = db.AssetStatus.Find(id);
            return View(assetstatus);
        }

        //
        // GET: /AssetStatus/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AssetStatus/Create

        [HttpPost]
        public ActionResult Create(AssetStatus assetstatus)
        {
            if (ModelState.IsValid)
            {
                db.AssetStatus.Add(assetstatus);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(assetstatus);
        }
        
        //
        // GET: /AssetStatus/Edit/5
 
        public ActionResult Edit(int id)
        {
            AssetStatus assetstatus = db.AssetStatus.Find(id);
            return View(assetstatus);
        }

        //
        // POST: /AssetStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(AssetStatus assetstatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetstatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assetstatus);
        }

        //
        // GET: /AssetStatus/Delete/5
 
        public ActionResult Delete(int id)
        {
            AssetStatus assetstatus = db.AssetStatus.Find(id);
            return View(assetstatus);
        }

        //
        // POST: /AssetStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            AssetStatus assetstatus = db.AssetStatus.Find(id);
            db.AssetStatus.Remove(assetstatus);
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