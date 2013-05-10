using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;

namespace AlisFirst.Controllers
{   
    public class LoansController : Controller
    {
		private readonly IAssetRepository assetRepository;
		private readonly IBorrowerRepository borrowerRepository;
		private readonly ILoanRepository loanRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public LoansController() : this(new AssetRepository(), new BorrowerRepository(), new LoanRepository())
        {
        }

        public LoansController(IAssetRepository assetRepository, IBorrowerRepository borrowerRepository, ILoanRepository loanRepository)
        {
			this.assetRepository = assetRepository;
			this.borrowerRepository = borrowerRepository;
			this.loanRepository = loanRepository;
        }

        //
        // GET: /Loans/

        public ViewResult Index()
        {
            return View(loanRepository.AllIncluding(loan => loan.Asset, loan => loan.Borrower));
        }

        //
        // GET: /Loans/Details/5

        public ViewResult Details(int id)
        {
            return View(loanRepository.Find(id));
        }

        //
        // GET: /Loans/Create

        public ActionResult Create()
        {
			ViewBag.PossibleAssets = assetRepository.All;
			ViewBag.PossibleBorrowers = borrowerRepository.All;
            return View();
        } 

        //
        // POST: /Loans/Create

        [HttpPost]
        public ActionResult Create(Loan loan)
        {
            if (ModelState.IsValid) {
                loanRepository.InsertOrUpdate(loan);
                loanRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAssets = assetRepository.All;
				ViewBag.PossibleBorrowers = borrowerRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Loans/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleAssets = assetRepository.All;
			ViewBag.PossibleBorrowers = borrowerRepository.All;
             return View(loanRepository.Find(id));
        }

        //
        // POST: /Loans/Edit/5

        [HttpPost]
        public ActionResult Edit(Loan loan)
        {
            if (ModelState.IsValid) {
                loanRepository.InsertOrUpdate(loan);
                loanRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAssets = assetRepository.All;
				ViewBag.PossibleBorrowers = borrowerRepository.All;
				return View();
			}
        }

        //
        // GET: /Loans/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(loanRepository.Find(id));
        }

        //
        // POST: /Loans/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            loanRepository.Delete(id);
            loanRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                assetRepository.Dispose();
                borrowerRepository.Dispose();
                loanRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

