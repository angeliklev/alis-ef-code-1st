using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.Areas
{   
    public class RepairsController : Controller
    {
		private readonly IAssetRepository assetRepository;
		private readonly IRepairRepository repairRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public RepairsController() : this(new AssetRepository(), new RepairRepository())
        {
        }

        public RepairsController(IAssetRepository assetRepository, IRepairRepository repairRepository)
        {
			this.assetRepository = assetRepository;
			this.repairRepository = repairRepository;
        }

        //
        // GET: /Repairs/

        public ViewResult Index()
        {
            return View(repairRepository.AllIncluding(repair => repair.Asset));
        }

        //
        // GET: /Repairs/Details/5

        public ViewResult Details(int id)
        {
            return View(repairRepository.Find(id));
        }

        //
        // GET: /Repairs/Create

        public ActionResult Create()
        {
			ViewBag.PossibleAssets = assetRepository.All;
            return View();
        } 

        //
        // POST: /Repairs/Create

        [HttpPost]
        public ActionResult Create(Repair repair)
        {
            if (ModelState.IsValid) {
                repairRepository.InsertOrUpdate(repair);
                repairRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAssets = assetRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Repairs/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleAssets = assetRepository.All;
             return View(repairRepository.Find(id));
        }

        //
        // POST: /Repairs/Edit/5

        [HttpPost]
        public ActionResult Edit(Repair repair)
        {
            if (ModelState.IsValid) {
                repairRepository.InsertOrUpdate(repair);
                repairRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAssets = assetRepository.All;
				return View();
			}
        }

        //
        // GET: /Repairs/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(repairRepository.Find(id));
        }

        //
        // POST: /Repairs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            repairRepository.Delete(id);
            repairRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                assetRepository.Dispose();
                repairRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

