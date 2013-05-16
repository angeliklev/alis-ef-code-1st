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
    public class BorrowersController : Controller
    {
        private AlisFirstContext context = new AlisFirstContext();

        //
        // GET: /Borrowers/

        public ViewResult Index()
        {
            return View(context.Borrowers.Include(borrower => borrower.Loans).Include(borrower => borrower.AssignedTos).ToList());
        }

        //
        // GET: /Borrowers/Details/5

        public ViewResult Details(int id)
        {
            Borrower borrower = context.Borrowers.Single(x => x.BorrowerID == id);
            return View(borrower);
        }

        //
        // GET: /Borrowers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Borrowers/Create

        [HttpPost]
        public ActionResult Create(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                context.Borrowers.Add(borrower);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(borrower);
        }
        
        //
        // GET: /Borrowers/Edit/5
 
        public ActionResult Edit(int id)
        {
            Borrower borrower = context.Borrowers.Single(x => x.BorrowerID == id);
            return View(borrower);
        }

        //
        // POST: /Borrowers/Edit/5

        [HttpPost]
        public ActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                context.Entry(borrower).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(borrower);
        }

        //
        // GET: /Borrowers/Delete/5
 
        public ActionResult Delete(int id)
        {
            Borrower borrower = context.Borrowers.Single(x => x.BorrowerID == id);
            return View(borrower);
        }

        //
        // POST: /Borrowers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrower borrower = context.Borrowers.Single(x => x.BorrowerID == id);
            context.Borrowers.Remove(borrower);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}