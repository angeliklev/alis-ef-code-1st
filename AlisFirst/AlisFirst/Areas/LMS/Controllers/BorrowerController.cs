using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;

namespace AlisFirst.Areas.LMS.Controllers
{ 
    public class BorrowerController : Controller
    {
        private AlisFirstDBContext db = new AlisFirstDBContext();

        //
        // GET: /LMS/Borrower/

        public ViewResult Index()
        {
            return View(db.Borrowers.ToList());
        }

        //
        // GET: /LMS/Borrower/Details/5

        public ViewResult Details(int id)
        {
            Borrower borrower = db.Borrowers.Find(id);
            return View(borrower);
        }

        //
        // GET: /LMS/Borrower/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /LMS/Borrower/Create

        [HttpPost]
        public ActionResult Create(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                db.Borrowers.Add(borrower);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(borrower);
        }
        
        //
        // GET: /LMS/Borrower/Edit/5
 
        public ActionResult Edit(int id)
        {
            Borrower borrower = db.Borrowers.Find(id);
            return View(borrower);
        }

        //
        // POST: /LMS/Borrower/Edit/5

        [HttpPost]
        public ActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrower).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(borrower);
        }

        //
        // GET: /LMS/Borrower/Delete/5
 
        public ActionResult Delete(int id)
        {
            Borrower borrower = db.Borrowers.Find(id);
            return View(borrower);
        }

        //
        // POST: /LMS/Borrower/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Borrower borrower = db.Borrowers.Find(id);
            db.Borrowers.Remove(borrower);
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