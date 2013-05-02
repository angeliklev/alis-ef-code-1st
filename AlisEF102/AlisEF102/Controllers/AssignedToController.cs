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
    public class AssignedToController : Controller
    {
        private AliseEFDBContext db = new AliseEFDBContext();

        //
        // GET: /AssignedTo/

        public ViewResult Index()
        {
            var assignedtoes = db.AssignedToes.Include(a => a.Asset).Include(a => a.Borrower);
            return View(assignedtoes.ToList());
        }

        //
        // GET: /AssignedTo/Details/5

        public ViewResult Details(int id)
        {
            AssignedTo assignedto = db.AssignedToes.Find(id);
            return View(assignedto);
        }

        //
        // GET: /AssignedTo/Create

        public ActionResult Create()
        {
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode");
            ViewBag.BorrowerID = new SelectList(db.Borrowers, "BorrowerID", "BarCode");
            return View();
        } 

        //
        // POST: /AssignedTo/Create

        [HttpPost]
        public ActionResult Create(AssignedTo assignedto)
        {
            if (ModelState.IsValid)
            {
                db.AssignedToes.Add(assignedto);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", assignedto.AssetID);
            ViewBag.BorrowerID = new SelectList(db.Borrowers, "BorrowerID", "BarCode", assignedto.BorrowerID);
            return View(assignedto);
        }
        
        //
        // GET: /AssignedTo/Edit/5
 
        public ActionResult Edit(int id)
        {
            AssignedTo assignedto = db.AssignedToes.Find(id);
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", assignedto.AssetID);
            ViewBag.BorrowerID = new SelectList(db.Borrowers, "BorrowerID", "BarCode", assignedto.BorrowerID);
            return View(assignedto);
        }

        //
        // POST: /AssignedTo/Edit/5

        [HttpPost]
        public ActionResult Edit(AssignedTo assignedto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignedto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", assignedto.AssetID);
            ViewBag.BorrowerID = new SelectList(db.Borrowers, "BorrowerID", "BarCode", assignedto.BorrowerID);
            return View(assignedto);
        }

        //
        // GET: /AssignedTo/Delete/5
 
        public ActionResult Delete(int id)
        {
            AssignedTo assignedto = db.AssignedToes.Find(id);
            return View(assignedto);
        }

        //
        // POST: /AssignedTo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            AssignedTo assignedto = db.AssignedToes.Find(id);
            db.AssignedToes.Remove(assignedto);
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