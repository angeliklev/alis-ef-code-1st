using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;

namespace AlisFirst.Controllers
{   
    public class AssignedToesController : Controller
    {
		private readonly IBorrowerRepository borrowerRepository;
		private readonly IAssetRepository assetRepository;
		private readonly IAssignedToRepository assignedtoRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AssignedToesController() : this(new BorrowerRepository(), new AssetRepository(), new AssignedToRepository())
        {
        }

        public AssignedToesController(IBorrowerRepository borrowerRepository, IAssetRepository assetRepository, IAssignedToRepository assignedtoRepository)
        {
			this.borrowerRepository = borrowerRepository;
			this.assetRepository = assetRepository;
			this.assignedtoRepository = assignedtoRepository;
        }

        //
        // GET: /AssignedToes/

        public ViewResult Index()
        {
            return View(assignedtoRepository.AllIncluding(assignedto => assignedto.Borrower, assignedto => assignedto.Asset));
        }

        //
        // GET: /AssignedToes/Details/5

        public ViewResult Details(int id)
        {
            return View(assignedtoRepository.Find(id));
        }

        //
        // GET: /AssignedToes/Create

        public ActionResult Create()
        {
			ViewBag.PossibleBorrowers = borrowerRepository.All;
			ViewBag.PossibleAssets = assetRepository.All;
            return View();
        } 

        //
        // POST: /AssignedToes/Create

        [HttpPost]
        public ActionResult Create(AssignedTo assignedto)
        {
            if (ModelState.IsValid) {
                assignedtoRepository.InsertOrUpdate(assignedto);
                assignedtoRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleBorrowers = borrowerRepository.All;
				ViewBag.PossibleAssets = assetRepository.All;
				return View();
			}
        }
        
        //
        // GET: /AssignedToes/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleBorrowers = borrowerRepository.All;
			ViewBag.PossibleAssets = assetRepository.All;
             return View(assignedtoRepository.Find(id));
        }

        //
        // POST: /AssignedToes/Edit/5

        [HttpPost]
        public ActionResult Edit(AssignedTo assignedto)
        {
            if (ModelState.IsValid) {
                assignedtoRepository.InsertOrUpdate(assignedto);
                assignedtoRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleBorrowers = borrowerRepository.All;
				ViewBag.PossibleAssets = assetRepository.All;
				return View();
			}
        }

        //
        // GET: /AssignedToes/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(assignedtoRepository.Find(id));
        }

        //
        // POST: /AssignedToes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            assignedtoRepository.Delete(id);
            assignedtoRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                borrowerRepository.Dispose();
                assetRepository.Dispose();
                assignedtoRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

