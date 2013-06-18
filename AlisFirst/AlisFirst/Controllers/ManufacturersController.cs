using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlisFirst.Models;
using AlisFirst.DAL;

namespace AlisFirst.Areas
{   
    public class ManufacturersController : Controller
    {
		private readonly IManufacturerRepository manufacturerRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ManufacturersController() : this(new ManufacturerRepository())
        {
        }

        public ManufacturersController(IManufacturerRepository manufacturerRepository)
        {
			this.manufacturerRepository = manufacturerRepository;
        }

        //
        // GET: /Manufacturers/

        public ViewResult Index()
        {
            return View(manufacturerRepository.AllIncluding(manufacturer => manufacturer.AssetModels));
        }

        //
        // GET: /Manufacturers/Details/5

        public ViewResult Details(int id)
        {
            return View(manufacturerRepository.Find(id));
        }

        //
        // GET: /Manufacturers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Manufacturers/Create

        [HttpPost]
        public ActionResult Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid) {
                manufacturerRepository.InsertOrUpdate(manufacturer);
                manufacturerRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Manufacturers/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(manufacturerRepository.Find(id));
        }

        //
        // POST: /Manufacturers/Edit/5

        [HttpPost]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid) {
                manufacturerRepository.InsertOrUpdate(manufacturer);
                manufacturerRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Manufacturers/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(manufacturerRepository.Find(id));
        }

        //
        // POST: /Manufacturers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            manufacturerRepository.Delete(id);
            manufacturerRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                manufacturerRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

