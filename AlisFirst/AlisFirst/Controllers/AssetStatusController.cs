using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.Areas
{   
    public class AssetStatusController : Controller
    {
		private readonly IAssetStatusRepository assetstatusRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AssetStatusController() : this(new AssetStatusRepository())
        {
        }

        public AssetStatusController(IAssetStatusRepository assetstatusRepository)
        {
			this.assetstatusRepository = assetstatusRepository;
        }

        //
        // GET: /AssetStatus/

        public ViewResult Index()
        {
            return View(assetstatusRepository.AllIncluding(assetstatus => assetstatus.AssignedStatuses));
        }

        //
        // GET: /AssetStatus/Details/5

        public ViewResult Details(int id)
        {
            return View(assetstatusRepository.Find(id));
        }

        //
        // GET: /AssetStatus/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AssetStatus/Create

        [HttpPost]
        public ActionResult Create(AssetStatus assetstatus)
        {
            if (ModelState.IsValid) {
                assetstatusRepository.InsertOrUpdate(assetstatus);
                assetstatusRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /AssetStatus/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(assetstatusRepository.Find(id));
        }

        //
        // POST: /AssetStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(AssetStatus assetstatus)
        {
            if (ModelState.IsValid) {
                assetstatusRepository.InsertOrUpdate(assetstatus);
                assetstatusRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /AssetStatus/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(assetstatusRepository.Find(id));
        }

        //
        // POST: /AssetStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            assetstatusRepository.Delete(id);
            assetstatusRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                assetstatusRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

