using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;

namespace AlisFirst.Controllers
{   
    public class AssignedStatusController : Controller
    {
		private readonly IAssetRepository assetRepository;
		private readonly IAssetStatusRepository assetstatusRepository;
		private readonly IAssignedStatusRepository assignedstatusRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AssignedStatusController() : this(new AssetRepository(), new AssetStatusRepository(), new AssignedStatusRepository())
        {
        }

        public AssignedStatusController(IAssetRepository assetRepository, IAssetStatusRepository assetstatusRepository, IAssignedStatusRepository assignedstatusRepository)
        {
			this.assetRepository = assetRepository;
			this.assetstatusRepository = assetstatusRepository;
			this.assignedstatusRepository = assignedstatusRepository;
        }

        //
        // GET: /AssignedStatus/

        public ViewResult Index()
        {
            return View(assignedstatusRepository.AllIncluding(assignedstatus => assignedstatus.Asset, assignedstatus => assignedstatus.AssetStatus));
        }

        //
        // GET: /AssignedStatus/Details/5

        public ViewResult Details(int id)
        {
            return View(assignedstatusRepository.Find(id));
        }

        //
        // GET: /AssignedStatus/Create

        public ActionResult Create()
        {
			ViewBag.PossibleAssets = assetRepository.All;
			ViewBag.PossibleAssetStatus = assetstatusRepository.All;
            return View();
        } 

        //
        // POST: /AssignedStatus/Create

        [HttpPost]
        public ActionResult Create(AssignedStatus assignedstatus)
        {
            if (ModelState.IsValid) {
                assignedstatusRepository.InsertOrUpdate(assignedstatus);
                assignedstatusRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAssets = assetRepository.All;
				ViewBag.PossibleAssetStatus = assetstatusRepository.All;
				return View();
			}
        }
        
        //
        // GET: /AssignedStatus/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleAssets = assetRepository.All;
			ViewBag.PossibleAssetStatus = assetstatusRepository.All;
             return View(assignedstatusRepository.Find(id));
        }

        //
        // POST: /AssignedStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(AssignedStatus assignedstatus)
        {
            if (ModelState.IsValid) {
                assignedstatusRepository.InsertOrUpdate(assignedstatus);
                assignedstatusRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAssets = assetRepository.All;
				ViewBag.PossibleAssetStatus = assetstatusRepository.All;
				return View();
			}
        }

        //
        // GET: /AssignedStatus/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(assignedstatusRepository.Find(id));
        }

        //
        // POST: /AssignedStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            assignedstatusRepository.Delete(id);
            assignedstatusRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                assetRepository.Dispose();
                assetstatusRepository.Dispose();
                assignedstatusRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

