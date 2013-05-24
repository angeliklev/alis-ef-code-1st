using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;
using AlisFirst.Validation;
using AlisFirst.ViewModels;

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
            Loan newLoan = new Loan();

            newLoan.LoanDate = DateTime.Now;
            return View(AutoMapper.Mapper.Map<Loan, CreateLoanViewModel>(newLoan));
        } 

        //
        // POST: /Loans/Create

        [HttpPost]
        public ActionResult Create(CreateLoanViewModel createLoan)
        {

            if (ModelState.IsValid)
            {
                Loan insertLoan = AutoMapper.Mapper.Map<CreateLoanViewModel, Loan>(createLoan);
                insertLoan.BorrowerID = loanRepository.getBorrowerID(createLoan.BorrowerBarcode);
                insertLoan.AssetID = loanRepository.getAssetID(createLoan.AssetBarcode);
                loanRepository.InsertOrUpdate(insertLoan);
                loanRepository.Save();
                return RedirectToAction("Index");
            }

            else
            {
                return View(createLoan);
            }
        }
        
        //
        // GET: /Loans/Edit/5
 
        public ActionResult Edit(int id)
        {
            Loan returnLoan = loanRepository.Find(id);
            //DateTime loandate = returnLoan.DueDate;
            returnLoan.LoanDate = returnLoan.LoanDate;
            returnLoan.ReturnDate = DateTime.Now;
            return View(AutoMapper.Mapper.Map<Loan, EditLoanViewModel>(returnLoan));
        }

        //
        // POST: /Loans/Edit/5

        [HttpPost]
        public ActionResult Edit(EditLoanViewModel returnLoan)
        {
            if (ModelState.IsValid)
            {
                Loan insertReturn = AutoMapper.Mapper.Map<EditLoanViewModel, Loan>(returnLoan);
                insertReturn.BorrowerID = loanRepository.getBorrowerID(returnLoan.BorrowerBarcode);
                insertReturn.AssetID = loanRepository.getAssetID(returnLoan.AssetBarcode);
                loanRepository.InsertOrUpdate(insertReturn);
                loanRepository.Save();


                return RedirectToAction("Index");
            }
            else
            {
                return View(returnLoan);
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

