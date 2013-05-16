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
        private BorrowerRepository context = new BorrowerRepository();

        //
        // GET: /Borrowers/

        public ViewResult Index()
        {
            return View(context.All.Include(borrower => borrower.Loans).Include(borrower => borrower.AssignedTos).ToList());
        }

        //
        // GET: /Borrowers/Details/5

        public ViewResult Details(int id)
        {
            Borrower borrower = context.All.Single(x => x.BorrowerID == id);
            return View(borrower);
        }

        //
        // GET: /Borrowers/Create

        public ActionResult Create()
        {
            Borrower NewBorrower = new Borrower();
            NewBorrower.BorrowerExpiryDate = DateTime.Now;
            return View( NewBorrower );
        } 

        //
        // POST: /Borrowers/Create

        [HttpPost]
        public ActionResult Create(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                context.InsertOrUpdate(borrower);
                context.Save();
                return RedirectToAction("Index");  
            }

            return View(borrower);
        }
        
        //
        // GET: /Borrowers/Edit/5
 
        public ActionResult Edit(int id)
        {
            Borrower borrower = context.Find( id );
            return View(borrower);
        }

        //
        // POST: /Borrowers/Edit/5

        [HttpPost]
        public ActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                context.InsertOrUpdate(borrower);
                context.Save();
                return RedirectToAction("Index");
            }
            return View(borrower);
        }

        //
        // GET: /Borrowers/Delete/5
 
        public ActionResult Delete(int id)
        {
            Borrower borrower = context.Find(id);
            return View(borrower);
        }

        //
        // POST: /Borrowers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrower borrower = context.Find(id);
            context.Delete( id );
            context.Save();
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