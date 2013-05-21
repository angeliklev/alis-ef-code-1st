using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.Controllers
{   
    public class AssetsController : Controller
    {
		private readonly ISupplierRepository supplierRepository;
        private readonly ICategoryRepository categoryRepository;
		private readonly IAssetModelRepository assetmodelRepository;
		private readonly IAssetRepository assetRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AssetsController() : this(new SupplierRepository(), new CategoryRepository(), new AssetModelRepository(), new AssetRepository())
        {
        }

        public AssetsController(ISupplierRepository supplierRepository, ICategoryRepository categoryRepository, IAssetModelRepository assetmodelRepository, IAssetRepository assetRepository)
        {
			this.supplierRepository = supplierRepository;
			this.categoryRepository = categoryRepository;
			this.assetmodelRepository = assetmodelRepository;
			this.assetRepository = assetRepository;
        }

        //
        // GET: /Assets/

        public ViewResult Index()
        {
            return View(assetRepository.AllIncluding(asset => asset.Supplier, asset => asset.Category, asset => asset.AssetModel, asset => asset.AssetConditions, asset => asset.CheckListItems, asset => asset.Repairs, asset => asset.Loans, asset => asset.AssignedToes, asset => asset.AssignedLocations));
        }

        //
        // GET: /Assets/Details/5

        public ViewResult Details(int id)
        {
            return View(assetRepository.Find(id));
        }

        //
        // GET: /Assets/Create

        public ActionResult Create()
        {
			ViewBag.PossibleSuppliers = supplierRepository.All;
            ViewBag.PossibleCategories = categoryRepository.All;
            ViewBag.PossibleAssetModels = assetmodelRepository.All;
            return View();
        } 

        //
        // POST: /Assets/Create

        [HttpPost]
        public ActionResult Create(Asset asset)
        {
            if (ModelState.IsValid) {
                assetRepository.InsertOrUpdate(asset);
                assetRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleSuppliers = supplierRepository.All;
                ViewBag.PossibleCategories = categoryRepository.All;
				ViewBag.PossibleAssetModels = assetmodelRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Assets/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleSuppliers = supplierRepository.All;
            ViewBag.PossibleCategories = categoryRepository.All;
			ViewBag.PossibleAssetModels = assetmodelRepository.All;
             return View(assetRepository.Find(id));
        }

        //
        // POST: /Assets/Edit/5

        [HttpPost]
        public ActionResult Edit(Asset asset)
        {
            if (ModelState.IsValid) {
                assetRepository.InsertOrUpdate(asset);
                assetRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleSuppliers = supplierRepository.All;
                ViewBag.PossibleCategories = categoryRepository.All;
				ViewBag.PossibleAssetModels = assetmodelRepository.All;
				return View();
			}
        }

        //
        // GET: /Assets/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(assetRepository.Find(id));
        }

        //
        // POST: /Assets/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            assetRepository.Delete(id);
            assetRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                supplierRepository.Dispose();
                categoryRepository.Dispose();
                assetmodelRepository.Dispose();
                assetRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

