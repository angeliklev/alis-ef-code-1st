using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

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
            Borrower EmptyBorrower = new Borrower();
            EmptyBorrower.BorrowerExpiryDate = DateTime.Now;
            return View( EmptyBorrower );
        } 

        //
        // POST: /Borrowers/Create

        [HttpPost]
        public ActionResult Create(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                if (context.CheckEmailUnique(borrower.Email))
                {
                    context.InsertOrUpdate(borrower);
                    context.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(borrower);
                }
 
            }

            return RedirectToAction("Index", "Home");
        }
        
        //
        // GET: /Borrowers/Edit/5
 
        public ActionResult Edit(int id)
        {
            Borrower borrower = context.All.Single(x => x.BorrowerID == id);
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
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Borrowers/Delete/5
 
        public ActionResult Delete(int id)
        {
            Borrower borrower = context.All.Single(x => x.BorrowerID == id);
            return View(borrower);
        }

        //
        // POST: /Borrowers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrower borrower = context.All.Single(x => x.BorrowerID == id);
            context.Delete(borrower.BorrowerID);
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