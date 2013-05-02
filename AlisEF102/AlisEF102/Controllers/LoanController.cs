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
    public class LoanController : Controller
    {
        private AliseEFDBContext db = new AliseEFDBContext();

        //
        // GET: /Loan/

        public ViewResult Index()
        {
            var loans = db.Loans.Include(l => l.Asset).Include(l => l.Borrower);
            return View(loans.ToList());
        }

        //
        // GET: /Loan/Details/5

        public ViewResult Details(int id)
        {
            Loan loan = db.Loans.Find(id);
            return View(loan);
        }

        //
        // GET: /Loan/Create

        public ActionResult Create()
        {
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode");
            ViewBag.BorrowerID = new SelectList(db.Borrowers, "BorrowerID", "BarCode");
            return View();
        } 

        //
        // POST: /Loan/Create

        [HttpPost]
        public ActionResult Create(Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Loans.Add(loan);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", loan.AssetID);
            ViewBag.BorrowerID = new SelectList(db.Borrowers, "BorrowerID", "BarCode", loan.BorrowerID);
            return View(loan);
        }
        
        //
        // GET: /Loan/Edit/5
 
        public ActionResult Edit(int id)
        {
            Loan loan = db.Loans.Find(id);
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", loan.AssetID);
            ViewBag.BorrowerID = new SelectList(db.Borrowers, "BorrowerID", "BarCode", loan.BorrowerID);
            return View(loan);
        }

        //
        // POST: /Loan/Edit/5

        [HttpPost]
        public ActionResult Edit(Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetID = new SelectList(db.Assets, "AssetID", "BarCode", loan.AssetID);
            ViewBag.BorrowerID = new SelectList(db.Borrowers, "BorrowerID", "BarCode", loan.BorrowerID);
            return View(loan);
        }

        //
        // GET: /Loan/Delete/5
 
        public ActionResult Delete(int id)
        {
            Loan loan = db.Loans.Find(id);
            return View(loan);
        }

        //
        // POST: /Loan/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
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