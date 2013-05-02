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
    public class SupplierController : Controller
    {
        private AliseEFDBContext db = new AliseEFDBContext();

        //
        // GET: /Supplier/

        public ViewResult Index()
        {
            return View(db.Suppliers.ToList());
        }

        //
        // GET: /Supplier/Details/5

        public ViewResult Details(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            return View(supplier);
        }

        //
        // GET: /Supplier/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Supplier/Create

        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(supplier);
        }
        
        //
        // GET: /Supplier/Edit/5
 
        public ActionResult Edit(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            return View(supplier);
        }

        //
        // POST: /Supplier/Edit/5

        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        //
        // GET: /Supplier/Delete/5
 
        public ActionResult Delete(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            return View(supplier);
        }

        //
        // POST: /Supplier/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
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