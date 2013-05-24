using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;
using AlisFirst.ViewModels;

namespace AlisFirst.Areas.LMS.Controllers
{   
    public class BorrowersController : Controller
    {
        private BorrowerRepository context = new BorrowerRepository();

        //
        // GET: /Borrowers/

        public ViewResult Index()
        {
            return View(AutoMapper.Mapper.Map<IEnumerable<Borrower>, IEnumerable<ListBorrowerViewModel>>(context.All));
        }

        //
        // GET: /Borrowers/Details/5

        public ViewResult Details(int id)
        {
            Borrower borrower = context.Find(id);
            return View(borrower);
        }

        //
        // GET: /Borrowers/Create

        public ActionResult Create()
        {
            Borrower EmptyBorrower = new Borrower();
            EmptyBorrower.BorrowerExpiryDate = DateTime.Now;
            return View(AutoMapper.Mapper.Map<Borrower, CreateBorrowerViewModel>(EmptyBorrower));
        } 

        //
        // POST: /Borrowers/Create

        [HttpPost]
        public ActionResult Create(CreateBorrowerViewModel borrowermodel)
        {
            Borrower borrower = AutoMapper.Mapper.Map<CreateBorrowerViewModel, Borrower>(borrowermodel);
            if (ModelState.IsValid)
            {
                context.InsertOrUpdate(borrower);
                context.Save();
                return RedirectToAction("Index");
 
            }
            else
            {
                return View(borrowermodel);
            }
        }
        
        //
        // GET: /Borrowers/Edit/5
 
        public ActionResult Edit(int id)
        {
            Borrower borrower = context.Find(id);
            return View(AutoMapper.Mapper.Map<Borrower, EditBorrowerViewModel>(borrower));
        }

        //
        // POST: /Borrowers/Edit/5

        [HttpPost]
        public ActionResult Edit(EditBorrowerViewModel borrowerModel)
        {
            Borrower borrower = AutoMapper.Mapper.Map<EditBorrowerViewModel, Borrower>(borrowerModel);
            if (ModelState.IsValid)
            {
                context.InsertOrUpdate(borrower);
                context.Save();
                return RedirectToAction("Index");
            }
            return View(borrowerModel);
        }

        //
        // GET: /Borrowers/Delete/5
 
        public ActionResult Delete(int id)
        {
            Borrower borrower = context.All.Single(x => x.BorrowerID == id);
            return View(AutoMapper.Mapper.Map<Borrower, DeleteBorrowerViewModel>(borrower));
        }

        //
        // POST: /Borrowers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrower borrower = context.Find(id);
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