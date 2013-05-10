using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;

namespace AlisFirst.Controllers
{   
    public class BorrowersController : Controller
    {
		private readonly IBorrowerRepository borrowerRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public BorrowersController() : this(new BorrowerRepository())
        {
        }

        public BorrowersController(IBorrowerRepository borrowerRepository)
        {
			this.borrowerRepository = borrowerRepository;
        }

        //
        // GET: /Borrowers/

        public ViewResult Index()
        {
            return View(borrowerRepository.AllIncluding(borrower => borrower.Loans, borrower => borrower.AssignedTos));
        }

        //
        // GET: /Borrowers/Details/5

        public ViewResult Details(int id)
        {
            return View(borrowerRepository.Find(id));
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
            if (ModelState.IsValid) {
                borrowerRepository.InsertOrUpdate(borrower);
                borrowerRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Borrowers/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(borrowerRepository.Find(id));
        }

        //
        // POST: /Borrowers/Edit/5

        [HttpPost]
        public ActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid) {
                borrowerRepository.InsertOrUpdate(borrower);
                borrowerRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Borrowers/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(borrowerRepository.Find(id));
        }

        //
        // POST: /Borrowers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            borrowerRepository.Delete(id);
            borrowerRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                borrowerRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

