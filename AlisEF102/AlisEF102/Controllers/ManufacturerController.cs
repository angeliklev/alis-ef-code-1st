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
    public class ManufacturerController : Controller
    {
        private AliseEFDBContext db = new AliseEFDBContext();

        //
        // GET: /Manufacturer/

        public ViewResult Index()
        {
            return View(db.Manufacturers.ToList());
        }

        //
        // GET: /Manufacturer/Details/5

        public ViewResult Details(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Manufacturer/Create

        [HttpPost]
        public ActionResult Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(manufacturer);
        }
        
        //
        // GET: /Manufacturer/Edit/5
 
        public ActionResult Edit(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            return View(manufacturer);
        }

        //
        // POST: /Manufacturer/Edit/5

        [HttpPost]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Delete/5
 
        public ActionResult Delete(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            return View(manufacturer);
        }

        //
        // POST: /Manufacturer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            db.Manufacturers.Remove(manufacturer);
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