using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.Areas
{   
    public class AssetConditionsController : Controller
    {
		private readonly IAssetRepository assetRepository;
		private readonly IAssetConditionRepository assetconditionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AssetConditionsController() : this(new AssetRepository(), new AssetConditionRepository())
        {
        }

        public AssetConditionsController(IAssetRepository assetRepository, IAssetConditionRepository assetconditionRepository)
        {
			this.assetRepository = assetRepository;
			this.assetconditionRepository = assetconditionRepository;
        }

        //
        // GET: /AssetConditions/

        public ViewResult Index()
        {
            return View(assetconditionRepository.AllIncluding(assetcondition => assetcondition.Asset));
        }

        //
        // GET: /AssetConditions/Details/5

        public ViewResult Details(int id)
        {
            return View(assetconditionRepository.Find(id));
        }

        //
        // GET: /AssetConditions/Create

        public ActionResult Create()
        {
			ViewBag.PossibleAssets = assetRepository.All;
            return View();
        } 

        //
        // POST: /AssetConditions/Create

        [HttpPost]
        public ActionResult Create(AssetCondition assetcondition)
        {
            if (ModelState.IsValid) {
                assetconditionRepository.InsertOrUpdate(assetcondition);
                assetconditionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAssets = assetRepository.All;
				return View();
			}
        }
        
        //
        // GET: /AssetConditions/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleAssets = assetRepository.All;
             return View(assetconditionRepository.Find(id));
        }

        //
        // POST: /AssetConditions/Edit/5

        [HttpPost]
        public ActionResult Edit(AssetCondition assetcondition)
        {
            if (ModelState.IsValid) {
                assetconditionRepository.InsertOrUpdate(assetcondition);
                assetconditionRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAssets = assetRepository.All;
				return View();
			}
        }

        //
        // GET: /AssetConditions/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(assetconditionRepository.Find(id));
        }

        //
        // POST: /AssetConditions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            assetconditionRepository.Delete(id);
            assetconditionRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                assetRepository.Dispose();
                assetconditionRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

