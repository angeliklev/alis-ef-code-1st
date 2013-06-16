using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.Controllers
{
    public class AssetModelsController : Controller
    {

        //Create the data sources
        private readonly IManufacturerRepository manufacturerRepository;
        private readonly IAssetModelRepository assetmodelRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public AssetModelsController()
            : this(new ManufacturerRepository(), new AssetModelRepository())
        {
        }

        //Constructor, instantiate the repos
        public AssetModelsController(IManufacturerRepository manufacturerRepository, IAssetModelRepository assetmodelRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
            this.assetmodelRepository = assetmodelRepository;
        }

        //
        // GET: /AssetModels/

        public ViewResult Index()
        {
            //Return the Index page with all data, including the manufacturers and assets.
            return View(assetmodelRepository.AllIncluding(assetmodel => assetmodel.Manufacturer, assetmodel => assetmodel.Assets));
        }

        //
        // GET: /AssetModels/Details/5

        public ViewResult Details(int id)
        {
            //Return the Detail page, with an asset matching the ID from the URI
            return View(assetmodelRepository.Find(id));
        }

        //
        // GET: /AssetModels/Create

        public ActionResult Create()
        {
            //Place all manufacturers into the viewbag for access in the view.
            ViewBag.PossibleManufacturers = manufacturerRepository.All;
            //Returns the create page on GET request
            return View();
        }

        //
        // POST: /AssetModels/Create

        //Uses Databinding to create an asset model
        [HttpPost]
        public ActionResult Create(AssetModel assetmodel)
        {
            //Checks if the model is valid
            if (ModelState.IsValid)
            {
                //If so it inserts it into the repo and saves.
                assetmodelRepository.InsertOrUpdate(assetmodel);
                assetmodelRepository.Save();
                //then returns to the Index page.
                return RedirectToAction("Index");
            }
            else
            {
                //if not, just returns to the Create page.
                ViewBag.PossibleManufacturers = manufacturerRepository.All;
                return View();
            }
        }

        //
        // GET: /AssetModels/Edit/5

        public ActionResult Edit(int id)
        {
            //Place all manufacturers into the viewbag for access in the view.
            ViewBag.PossibleManufacturers = manufacturerRepository.All;
            //Return the Edit page, with an asset matching the ID from the URI
            return View(assetmodelRepository.Find(id));
        }

        //
        // POST: /AssetModels/Edit/5

        [HttpPost]
        public ActionResult Edit(AssetModel assetmodel)
        {
            //Checks if the model is valid
            if (ModelState.IsValid)
            {
                //If so it updates it in the repo and saves.
                assetmodelRepository.InsertOrUpdate(assetmodel);
                assetmodelRepository.Save();
                //then returns to the Index page.
                return RedirectToAction("Index");
            }
            else
            {
                //if not, just returns to the Edit page.
                ViewBag.PossibleManufacturers = manufacturerRepository.All;
                return View();
            }
        }

        //
        // GET: /AssetModels/Delete/5

        public ActionResult Delete(int id)
        {
            //Return the Delete page, with an asset matching the ID from the URI
            return View(assetmodelRepository.Find(id));
        }

        //
        // POST: /AssetModels/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Deletes the Asset with matching id from the URI and saves the changes
            assetmodelRepository.Delete(id);
            assetmodelRepository.Save();
            //Redirects to the index page.
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                manufacturerRepository.Dispose();
                assetmodelRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

