using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;

namespace AlisFirst.Controllers
{   
    public class AssetModelsController : Controller
    {
		private readonly IManufacturerRepository manufacturerRepository;
		private readonly ICategoryRepository categoryRepository;
		private readonly IAssetModelRepository assetmodelRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AssetModelsController() : this(new ManufacturerRepository(), new CategoryRepository(), new AssetModelRepository())
        {
        }

        public AssetModelsController(IManufacturerRepository manufacturerRepository, ICategoryRepository categoryRepository, IAssetModelRepository assetmodelRepository)
        {
			this.manufacturerRepository = manufacturerRepository;
			this.categoryRepository = categoryRepository;
			this.assetmodelRepository = assetmodelRepository;
        }

        //
        // GET: /AssetModels/

        public ViewResult Index()
        {
            return View(assetmodelRepository.AllIncluding(assetmodel => assetmodel.Manufacturer, assetmodel => assetmodel.Category, assetmodel => assetmodel.Assets));
        }

        //
        // GET: /AssetModels/Details/5

        public ViewResult Details(int id)
        {
            return View(assetmodelRepository.Find(id));
        }

        //
        // GET: /AssetModels/Create

        public ActionResult Create()
        {
			ViewBag.PossibleManufacturers = manufacturerRepository.All;
			ViewBag.PossibleCategories = categoryRepository.All;
            return View();
        } 

        //
        // POST: /AssetModels/Create

        [HttpPost]
        public ActionResult Create(AssetModel assetmodel)
        {
            if (ModelState.IsValid) {
                assetmodelRepository.InsertOrUpdate(assetmodel);
                assetmodelRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleManufacturers = manufacturerRepository.All;
				ViewBag.PossibleCategories = categoryRepository.All;
				return View();
			}
        }
        
        //
        // GET: /AssetModels/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleManufacturers = manufacturerRepository.All;
			ViewBag.PossibleCategories = categoryRepository.All;
             return View(assetmodelRepository.Find(id));
        }

        //
        // POST: /AssetModels/Edit/5

        [HttpPost]
        public ActionResult Edit(AssetModel assetmodel)
        {
            if (ModelState.IsValid) {
                assetmodelRepository.InsertOrUpdate(assetmodel);
                assetmodelRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleManufacturers = manufacturerRepository.All;
				ViewBag.PossibleCategories = categoryRepository.All;
				return View();
			}
        }

        //
        // GET: /AssetModels/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(assetmodelRepository.Find(id));
        }

        //
        // POST: /AssetModels/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            assetmodelRepository.Delete(id);
            assetmodelRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                manufacturerRepository.Dispose();
                categoryRepository.Dispose();
                assetmodelRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

